using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Attendance.Classes;
using MQTTnet;
using MQTTnet.Client;
using Quartz.Impl;
using Quartz;
using Quartz.Impl.Matchers;
using System.IO;


namespace Attendance.Forms
{
    public partial class frmServerStatus : Form
    {

        // This delegate enables asynchronous calls for setting
        // the text property on a TextBox control.
        delegate void SetTextCallback(string text);
        private static string pcname = Utils.Helper.GetLocalPCName();
        private static IMqttClient mqtc;
        private static string TraceFilePath = Utils.Helper.GetTraceFilePath();

        public frmServerStatus()
        {
            InitializeComponent();

        }


        public void SetText(string text)
        {
            try
            {
                // InvokeRequired required compares the thread ID of the
                // calling thread to the thread ID of the creating thread.
                // If these threads are different, it returns true.
                if (this.rtxtLoginMessage.InvokeRequired)
                {
                    //SetTextCallback d = new SetTextCallback(SetText);
                    //this.Invoke(d, new object[] {  text });
                    this.rtxtLoginMessage.Invoke((Action)(() => SetText(text)));
                }
                else
                {
                    if (this.rtxtLoginMessage.Lines.Count() > 300)
                        this.rtxtLoginMessage.Text = "";

                    rtxtLoginMessage.Select(0, 0);
                    rtxtLoginMessage.SelectedText = text + Environment.NewLine;

                    string filenminfo = "ServerStatus_Info_" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                    string fullpath2 = Path.Combine(TraceFilePath, filenminfo);
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(fullpath2, true))
                    {
                        file.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "-" + text);
                    }

                }
            }
            catch (Exception ex)
            {
                if (mqtc.IsConnected)
                {
                    mqtc.Disconnected -= mqtc_Disconnected;
                    mqtc.ApplicationMessageReceived -= mqtc_MsgEventHandler;
                    mqtc.Connected -= mqtc_Connected;
                    mqtc.DisconnectAsync();
                }
            }

        }

        private void frmServerStatus_Load(object sender, EventArgs evt)
        {
            Connect();
            lblServer.Text = Globals.G_ServerWorkerIP;
        }


        private void frmServerStatus_FormClosing(object sender, FormClosingEventArgs e)
        {

            mqtc.Disconnected -= mqtc_Disconnected;
            mqtc.ApplicationMessageReceived -= mqtc_MsgEventHandler;
            mqtc.Connected -= mqtc_Connected;


            if (mqtc.IsConnected)
            {
                mqtc.DisconnectAsync();
            }
        }


        private void Connect()
        {

            var clientoptions = new MqttClientOptionsBuilder()
            .WithTcpServer(Globals.G_ServerWorkerIP, 1884) // Port is optional
            .Build();

            mqtc = new MqttFactory().CreateMqttClient();
            mqtc.ConnectAsync(clientoptions);

            mqtc.Connected += mqtc_Connected;
            
            //mqtc.Connected += async (s, evt) =>
            //{
            //    SetText("### CONNECTED WITH SERVER ###" + Environment.NewLine);
            //    await mqtc.SubscribeAsync(new TopicFilterBuilder().WithTopic("Server/Status").Build());
            //    SetText("### SUBSCRIBED ###" + Environment.NewLine);
            //};


            mqtc.Disconnected += mqtc_Disconnected;

            //mqtc.Disconnected += async (s, evtdisconnected) =>
            //{
            //    SetText("### DISCONNECTED FROM SERVER ###" + Environment.NewLine);
            //    await Task.Delay(TimeSpan.FromSeconds(5));
            //    try
            //    {
            //        await mqtc.ConnectAsync(clientoptions);
            //    }
            //    catch
            //    {
            //        SetText("### RECONNECTING FAILED ### PLEASE CONTACT SERVER ADMINISTRATOR" + Environment.NewLine);

            //    }
            //};

            mqtc.ApplicationMessageReceived += mqtc_MsgEventHandler;

            //mqtc.ApplicationMessageReceived += (s, msgreceived) =>
            //{
            //    //SetText("### RECEIVED APPLICATION MESSAGE ###" + Environment.NewLine);
            //    SetText(string.Format("{0} Received : {1}", pcname, Encoding.UTF8.GetString(msgreceived.ApplicationMessage.Payload)));

            //};

        }

        private void mqtc_Connected(object s,MqttClientConnectedEventArgs e)
        {
            SetText("### CONNECTED WITH SERVER ###" + Environment.NewLine);
            mqtc.SubscribeAsync(new TopicFilterBuilder().WithTopic("Server/Status").Build());
            SetText("### SUBSCRIBED ###" + Environment.NewLine);
        }

        private void mqtc_Disconnected(object s, MqttClientDisconnectedEventArgs e)
        {
            var clientoptions = new MqttClientOptionsBuilder()
            .WithTcpServer(Globals.G_ServerWorkerIP, 1884) // Port is optional
            .Build();
            
            SetText("### DISCONNECTED FROM SERVER ###" + Environment.NewLine);
            Task.Delay(TimeSpan.FromSeconds(5));
            try
            {
                mqtc.ConnectAsync(clientoptions);
            }
            catch
            {
                SetText("### RECONNECTING FAILED ### PLEASE CONTACT SERVER ADMINISTRATOR" + Environment.NewLine);

            }
        }

        private void mqtc_MsgEventHandler(object s, MqttApplicationMessageReceivedEventArgs msgreceived)
        {
            SetText(string.Format("{0} Received : {1}", pcname, Encoding.UTF8.GetString(msgreceived.ApplicationMessage.Payload)));
        }

        private void btnReloadJob_Click(object sender, EventArgs e)
        {
            if(Utils.User.GUserID == "SERVER")
            {
                IScheduler sch = Globals.G_myscheduler.GetScheduler();
                DataTable JobDt = GetAllJobs(sch);
                grd_Upload.DataSource = JobDt;
                grd_Upload.Refresh();                
            }
            else
            {
                MessageBox.Show("Only Server can Access this feature..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static DataTable GetAllJobs(IScheduler scheduler)
        {

            DataTable Dt = new DataTable();
            Dt.Columns.Add("JobGroup", typeof(string));
            Dt.Columns.Add("JobKey", typeof(string));
            Dt.Columns.Add("JobDetails", typeof(string));
            Dt.Columns.Add("TriggerName", typeof(string));
            Dt.Columns.Add("TriggerGroup", typeof(string));
            Dt.Columns.Add("TriggerType", typeof(string));
            Dt.Columns.Add("TriggerStats", typeof(string));
            Dt.Columns.Add("NxtFireTime", typeof(string));
            Dt.Columns.Add("PrvFireTime", typeof(string));

            IList<string> jobGroups = scheduler.GetJobGroupNames();
            // IList<string> triggerGroups = scheduler.GetTriggerGroupNames();

            foreach (string group in jobGroups)
            {
                var groupMatcher = GroupMatcher<JobKey>.GroupContains(group);
                var jobKeys = scheduler.GetJobKeys(groupMatcher);
                foreach (var jobKey in jobKeys)
                {
                    var detail = scheduler.GetJobDetail(jobKey);
                    var triggers = scheduler.GetTriggersOfJob(jobKey);
                    foreach (ITrigger trigger in triggers)
                    {
                        DataRow dr = Dt.NewRow();
                        dr["JobGroup"] = group;
                        dr["JobKey"] = jobKey.Name;
                        dr["JobDetails"] = detail.Description;
                        dr["TriggerName"] = trigger.Key.Name;
                        dr["TriggerGroup"] = trigger.Key.Group;
                        dr["TriggerType"] = trigger.GetType().Name;
                        dr["TriggerStats"] = scheduler.GetTriggerState(trigger.Key);
                       
                        
                        
                        //dr["NxtFireTime"] = trigger.GetNextFireTimeUtc();
                        //dr["PrvFireTime"] = trigger.GetPreviousFireTimeUtc();
                        
                        
                        DateTimeOffset? nextFireTime = trigger.GetNextFireTimeUtc();
                        if (nextFireTime.HasValue)
                        {
                            dr["NxtFireTime"] = Convert.ToDateTime(nextFireTime.Value.LocalDateTime).ToString("yyyy-MM-dd HH:mm:ss");
                        }


                        DateTimeOffset? previousFireTime = trigger.GetPreviousFireTimeUtc();
                        if (previousFireTime.HasValue)
                        {
                            dr["PrvFireTime"] = Convert.ToDateTime(previousFireTime.Value.LocalDateTime).ToString("yyyy-MM-dd HH:mm:ss");
                        }

                        Dt.Rows.Add(dr);
                    }
                }
            }

            return Dt;
        }

        private void btnRestartSch_Click(object sender, EventArgs e)
        {
            if (Utils.User.GUserID == "SERVER")
            {
                this.Cursor = Cursors.WaitCursor;
                Globals.G_myscheduler.Restart();
                this.Cursor = Cursors.Default;
                
            }
            else
            {
                MessageBox.Show("Only Server can Access this feature..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
