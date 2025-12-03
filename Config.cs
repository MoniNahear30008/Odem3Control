using Org.BouncyCastle.Tls.Crypto.Impl.BC;
using System;
using System.Collections.Generic;
using System.Text;

namespace OdemControl
{
    public partial class Form1
    {
        // Sensitivity --> Normal: 0x81010E3C; High: 0x80010F3c	
        List<uint> sensitivity = new List<uint>() { 0x81010E3C, 0x80010F3c };
        public Dictionary<int, uint> WriteRegs = new Dictionary<int, uint>()
        {
            {(int)confStates.SEND_CAPTURE_DELAY, 0xFF200024 },
            {(int)confStates.RESET_DSP, 0xFF200010 },
            {(int)confStates.SET_SENSITIVITY, 0xFF200010 },
            {(int)confStates.SET_RANGE, 0xFF20007C},
            {(int)confStates.SET_RETRO_LEVEL, 0xFF200070},
            {(int)confStates.SET_VECTOR_1, 0xFF200028},
            {(int)confStates.SET_VECTOR_2, 0xFF20002C},
            {(int)confStates.SET_VECTOR_3, 0xFF248000},
            {(int)confStates.SET_VECTOR_4, 0xFF248080},
            {(int)confStates.SET_VECTOR_5, 0xFF340000},
            {(int)confStates.SET_VECTOR_6, 0xFF346000},
            {(int)confStates.SET_OT_DELAY, 0xFF20003C}
        };
        public int lastOTdelay = 0;

        private void cofigdevice()
        {
            string Error = "";
            while (confState != (int)confStates.DONE)
            {
                switch (confState)
                {
                    case (int)confStates.IDLE:
                        LogMessage("Configuring device");
                        deviceState.Text = "Configuring device";
                        this.Refresh();
                        confState++;
                        break;

                    case (int)confStates.SEND_CAPTURE_DELAY:
                        LogMessage("Configuring: Capture_Delay");
                        if (debugmodeEnabled)
                            deviceState.Text = "Configuring: Capture_Delay";
                        this.Refresh();

                        Error = WriteRegWaitResp(WriteRegs[(int)confStates.SEND_CAPTURE_DELAY], new List<uint> { (uint)deviceParameters["Capture_Delay"] });
                        if (Error.Length > 0)
                        {
                            LogMessage("Configuring Error: " + Error);
                            MessageBox.Show("Error sending Capture Delay:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        confState++;
                        break;

                    case (int)confStates.RESET_DSP:
                        LogMessage("Configuring: Reset DSP");
                        Error = WriteRegWaitResp(WriteRegs[(int)confStates.RESET_DSP], new List<uint> { 0x4100004 });
                        if (Error.Length > 0)
                        {
                            LogMessage("Configuring Error: " + Error);
                            MessageBox.Show("Error sending Reset DSP:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        confState++;
                        break;

                    case (int)confStates.SET_SENSITIVITY:
                        LogMessage("Configuring: Sensitivity");
                        if (debugmodeEnabled)
                            deviceState.Text = "Configuring: Sensitivity";
                        this.Refresh();

                        Error = WriteRegWaitResp(WriteRegs[(int)confStates.SET_SENSITIVITY], new List<uint> { sensitivity[appSetting.sensitivity] });
                        if (Error.Length > 0)
                        {
                            LogMessage("Configuring Error: " + Error);
                            MessageBox.Show("Error sending Set Senitivity:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        confState++;
                        break;

                    case (int)confStates.SET_RANGE:  // CFAR multiplication
                        LogMessage("Configuring: CFAR multiplication");
                        if (debugmodeEnabled)
                            deviceState.Text = "Configuring: multiplication";
                        this.Refresh();

                        Error = WriteRegWaitResp(WriteRegs[(int)confStates.SET_RANGE], new List<uint> { 0x00000808 });
                        if (Error.Length > 0)
                        {
                            LogMessage("Configuring Error: " + Error);
                            MessageBox.Show("Error sending Set Range:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        confState++;
                        break;

                    case (int)confStates.SET_RETRO_LEVEL:
                        LogMessage("Configuring: Retro level");
                        if (debugmodeEnabled)
                            deviceState.Text = "Configuring: Retro level";
                        this.Refresh();

                        Error = WriteRegWaitResp(WriteRegs[(int)confStates.SET_RETRO_LEVEL], new List<uint> { 10000 });
                        if (Error.Length > 0)
                        {
                            LogMessage("Configuring Error: " + Error);
                            MessageBox.Show("Error sending Set Relro:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        confState++;
                        break;

                    case (int)confStates.SET_CHIRP_WAVEFORM:
                        LogMessage("Configuring: Load AWG waveform");
                        if (debugmodeEnabled)
                            deviceState.Text = "Configuring: Load AWG waveform";
                        this.Refresh();

                        Error = SPIWriteAWGWaitResp(confFiles["AWG"] );
                        if (Error.Length > 0)
                        {
                            LogMessage("Configuring Error: " + Error);
                            MessageBox.Show("Error sending badGoodIndxs_High:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        confState++;
                        break;

                    case (int)confStates.SET_CHIRP_GAIN:
                        LogMessage("Configuring: SET_CHIRP_GAIN");
                        if (debugmodeEnabled)
                            deviceState.Text = "Configuring: SET_CHIRP_GAIN";
                        this.Refresh();

                        Error = WriteI2CWaitResp(3, 0x4B, 0x14, 0x1C, new List<uint> { (uint)deviceParameters["Chirp_AWG_gain"] });
                        if (Error.Length > 0)
                        {
                            LogMessage("Configuring Error: " + Error);
                            MessageBox.Show("Error sending badGoodIndxs_High:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        confState++;
                        break;
                     case (int)confStates.SET_PM_CONTROL:
                        LogMessage("Configuring: SET_PM1_CONTROL");
                        if (debugmodeEnabled)
                            deviceState.Text = "Configuring: SET_PM1_CONTROL";
                        this.Refresh();
                        Error = WriteI2CWaitResp(3, 0x4A, 0x14, 0x1C, new List<uint> { 0 });
                        if (Error.Length > 0)
                        {
                            LogMessage("Configuring Error: " + Error);
                            MessageBox.Show("Error sending badGoodIndxs_High:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        LogMessage("Configuring: SET_PM2_CONTROL");
                        if (debugmodeEnabled)
                            deviceState.Text = "Configuring: SET_PM2_CONTROL";
                        this.Refresh();
                        Error = WriteI2CWaitResp(3, 0x48, 0x14, 0x1C, new List<uint> { 1 });
                        if (Error.Length > 0)
                        {
                            LogMessage("Configuring Error: " + Error);
                            MessageBox.Show("Error sending badGoodIndxs_High:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        confState++;
                        break;

                    case (int)confStates.LOAD_SSH_DRIVER:
                        LogMessage("Configuring: LOAD_SSH_DRIVER");
                        if (debugmodeEnabled)
                            deviceState.Text = "Configuring: LOAD_SSH_DRIVER";
                        this.Refresh();
                        Error = LoadHHSDriver();
                        if (Error.Length > 0)
                        {
                            LogMessage("Configuring Error: " + Error);
                            MessageBox.Show("Error loading HHS driver:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        // insmod /lib/modules/$(uname -r)/extra/altera_msgdma_st.ko udp_forwarding=1 udp_dest_ip="192.168.2.20" udp_dest_port=10003 transfer_size=704
                        // ['ssh', '-o', 'ConnectTimeout=5', '-o', 'StrictHostKeyChecking=no', '-o', 'UserKnownHostsFile=/dev/null', '-o', 'LogLevel=ERROR', '-o', 'BatchMode=yes', 'root@192.168.2.24', 'insmod /lib/modules/$(uname -r)/extra/altera_msgdma_st.ko udp_forwarding=1 udp_dest_i....20" udp_dest_port=10003 transfer_size=704']
                        confState++;
                        break;

                    case (int)confStates.SET_LO:
                        LogMessage("Configuring: SET_LO");
                        if (debugmodeEnabled)
                            deviceState.Text = "Configuring: SET_LO";
                        this.Refresh();
                        Error = WriteI2CWaitResp(7, 0x4B, 0x14, 0x1C, new List<uint> { (uint)deviceParameters["LO"] });
                        if (Error.Length > 0)
                        {
                            LogMessage("Configuring Error: " + Error);
                            MessageBox.Show("Error sending badGoodIndxs_High:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        confState++;
                        break;

                    case (int)confStates.SET_TX_SOA1:
                        LogMessage("Configuring: SET_TX_SOA1");
                        if (debugmodeEnabled)
                            deviceState.Text = "Configuring: SET_TX_SOA1";
                        this.Refresh();
                        Error = WriteI2CWaitResp(7, 0x4A, 0x14, 0x19, new List<uint> { (uint)deviceParameters["TxSOA1"] });
                        if (Error.Length > 0)
                        {
                            LogMessage("Configuring Error: " + Error);
                            MessageBox.Show("Error sending badGoodIndxs_High:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        confState++;
                        break;

                    case (int)confStates.SET_TX_SOA2:
                        LogMessage("Configuring: SET_TX_SOA2");
                        if (debugmodeEnabled)
                            deviceState.Text = "Configuring: SET_TX_SOA2";
                        this.Refresh();
                        Error = WriteI2CWaitResp(7, 0x4A, 0x14, 0x1C, new List<uint> { (uint)deviceParameters["TxSOA2"] });
                        if (Error.Length > 0)
                        {
                            LogMessage("Configuring Error: " + Error);
                            MessageBox.Show("Error sending badGoodIndxs_High:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        confState++;
                        break;

                    case (int)confStates.SET_TX3_0_9:
                        LogMessage("Configuring: SET_TX3_0_9");
                        if (debugmodeEnabled)
                            deviceState.Text = "Configuring: SET_TX3_0_9";
                        this.Refresh();
                        Error = WriteI2CWaitResp(7, 0x49, 0x14, 0x19, new List<uint> { (uint)deviceParameters["Tx3_0_9"] });
                        if (Error.Length > 0)
                        {
                            LogMessage("Configuring Error: " + Error);
                            MessageBox.Show("Error sending badGoodIndxs_High:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        confState++;
                        break;

                    case (int)confStates.SET_TX3_10_19:
                        LogMessage("Configuring: SET_TX3_10_19");
                        if (debugmodeEnabled)
                            deviceState.Text = "Configuring: SET_TX3_10_19";
                        this.Refresh();
                        Error = WriteI2CWaitResp(7, 0x49, 0x14, 0x1C, new List<uint> { (uint)deviceParameters["Tx3_10_19"] });
                        if (Error.Length > 0)
                        {
                            LogMessage("Configuring Error: " + Error);
                            MessageBox.Show("Error sending badGoodIndxs_High:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        confState++;
                        break;

                    case (int)confStates.SET_TX3_20_29:
                        LogMessage("Configuring: SET_TX3_20_29");
                        if (debugmodeEnabled)
                            deviceState.Text = "Configuring: SET_TX3_20_29";
                        this.Refresh();
                        Error = WriteI2CWaitResp(7, 0x48, 0x14, 0x19, new List<uint> { (uint)deviceParameters["Tx3_20_29"] });
                        if (Error.Length > 0)
                        {
                            LogMessage("Configuring Error: " + Error);
                            MessageBox.Show("Error sending badGoodIndxs_High:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        confState++;
                        break;

                    case (int)confStates.SET_TX3_30_39:
                        LogMessage("Configuring: SET_TX3_30_39");
                        if (debugmodeEnabled)
                            deviceState.Text = "Configuring: SET_TX3_30_39";
                        this.Refresh();
                        Error = WriteI2CWaitResp(7, 0x48, 0x14, 0x1C, new List<uint> { (uint)deviceParameters["Tx3_30_39"] });
                        if (Error.Length > 0)
                        {
                            LogMessage("Configuring Error: " + Error);
                            MessageBox.Show("Error sending badGoodIndxs_High:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        confState++;
                        break;

                    case (int)confStates.SET_VECTOR_1:
                        LogMessage("Configuring: badGoodIndxs_High");
                        if (debugmodeEnabled)
                            deviceState.Text = "Configuring: badGoodIndxs_High";
                        this.Refresh();
                        Error = WriteRegWaitResp(WriteRegs[(int)confStates.SET_VECTOR_1], confFiles["badGoodIndxs_High"]);
                        if (Error.Length > 0)
                        {
                            LogMessage("Configuring Error: " + Error);
                            MessageBox.Show("Error sending badGoodIndxs_High:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        confState++;
                        break;

                    case (int)confStates.SET_VECTOR_2:
                        LogMessage("Configuring: badGoodIndxs_Low");
                        if (debugmodeEnabled)
                            deviceState.Text = "Configuring: badGoodIndxs_Low";
                        this.Refresh();
                        Error = WriteRegWaitResp(WriteRegs[(int)confStates.SET_VECTOR_2], confFiles["badGoodIndxs_Low"]);
                        if (Error.Length > 0)
                        {
                            LogMessage("Configuring Error: " + Error);
                            MessageBox.Show("Error sending badGoodIndxs_Low:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        confState++;
                        break;

                    case (int)confStates.SET_VECTOR_3:
                        if (debugmodeEnabled)
                        LogMessage("Configuring: 128Bins_Final to 0xFF248000");
                        if (debugmodeEnabled)
                            deviceState.Text = "Configuring: 128Bins_Final";
                        this.Refresh();
                        Error = WriteRegWaitResp(WriteRegs[(int)confStates.SET_VECTOR_3], confFiles["128Bins_Final"]);
                        if (Error.Length > 0)
                        {
                            LogMessage("Configuring Error: " + Error);
                            MessageBox.Show("Error sending 128Bins_Final:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        confState++;
                        break;

                    case (int)confStates.SET_VECTOR_4:
                        LogMessage("Configuring: 128Bins_Final to 0xFF248080");
                        Error = WriteRegWaitResp(WriteRegs[(int)confStates.SET_VECTOR_4], confFiles["128Bins_Final"]);
                        if (Error.Length > 0)
                        {
                            LogMessage("Configuring Error: " + Error);
                            MessageBox.Show("Error sending 128Bins_Final:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        confState++;
                        break;

                    case (int)confStates.SET_VECTOR_5:
                        LogMessage("Configuring: blackmanHarris_DEC");
                        if (debugmodeEnabled)
                            deviceState.Text = "Configuring: blackmanHarris_DEC";
                        this.Refresh();
                        Error = WriteRegWaitResp(WriteRegs[(int)confStates.SET_VECTOR_5], confFiles["blackmanHarris_DEC"]);
                        if (Error.Length > 0)
                        {
                            LogMessage("Configuring Error: " + Error);
                            MessageBox.Show("Error sending blackmanHarris_DEC:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        confState++;
                        break;

                    case (int)confStates.SET_VECTOR_6:
                        LogMessage("Configuring: 2kWin");
                        if (debugmodeEnabled)
                            deviceState.Text = "Configuring: 2kWin";
                        this.Refresh();
                        Error = WriteRegWaitResp(WriteRegs[(int)confStates.SET_VECTOR_6], confFiles["2kWin"]);
                        if (Error.Length > 0)
                        {
                            LogMessage("Configuring Error: " + Error);
                            MessageBox.Show("Error sending 2kWin:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        confState++;
                        break;

                    case (int)confStates.LOAD_FILES:
                        LogMessage("Configuring: Load files");
                        if (debugmodeEnabled)
                            deviceState.Text = "Configuring: Load files";
                        this.Refresh();
                        Error = LoadFiles();
                        if (Error.Length > 0)
                        {
                            LogMessage("Configuring Error: " + Error);
                            MessageBox.Show("Error Load files:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        confState++;
                        break;

                    case (int)confStates.SET_OT_DELAY:
                        LogMessage("Configuring: Set OT Delay");
                        if (debugmodeEnabled)
                            deviceState.Text = "Configuring: Set OT Delay";
                        this.Refresh();
                        string mode = modes[appSetting.scanModeNum];
                        int nPoints = scanModes[mode].nPoints;
                        int otd = deviceParameters[mode];
                        uint iotd = (uint)(nPoints - otd);
                        lastOTdelay = otd;
                        Error = WriteRegWaitResp(WriteRegs[(int)confStates.SET_OT_DELAY], new List<uint> { iotd });
                        if (Error.Length > 0)
                        {
                            LogMessage("Configuring Error: " + Error);
                            MessageBox.Show("Error sending  Set OT Delay:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        confState++;
                        break;

                    case (int)confStates.RUN_OPTOTUNE_CALIBRATION:
                        LogMessage("Configuring: Run opto");
                        if (debugmodeEnabled)
                            deviceState.Text = "Start Odem (~40Sec)";
                        Error = RunOpto(scanModes[modes[appSetting.scanModeNum]].modeNum);
                        optoStat.Visible = false;
                        this.Refresh();
                        if (Error.Length > 0)
                        {
                            LogMessage("Configuring Error: " + Error);
                            MessageBox.Show("Error sending 2kWin:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        confState++;
                        break;

                    case (int)confStates.DONE:
                        deviceState.Text = "Device ready";
                        this.Refresh();
                        break;

                    default:
                        MessageBox.Show("Configuration is already in progress.", "Configuration In Progress", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                }
            }
        }

    }
}
