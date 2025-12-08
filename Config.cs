using Org.BouncyCastle.Tls.Crypto.Impl.BC;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OdemControl
{
    public partial class Form1
    {
        List<uint> sensitivity = new List<uint>() { 0x81010E3C, 0x81010F3c };
        public Dictionary<int, uint> WriteRegs = new Dictionary<int, uint>()
        {
            {(int)confStates.SEND_CAPTURE_DELAY, 0xFF200024 },
            {(int)confStates.RESET_DSP, 0xFF200010 },
            {(int)confStates.SET_SENSITIVITY, 0xFF200010 },
            {(int)confStates.SET_RANGE, 0xFF20007C},
            {(int)confStates.SET_SPUR, 0xFF200074},
            {(int)confStates.SET_RETRO_LEVEL, 0xFF200070},
            {(int)confStates.SET_VECTOR_1, 0xFF200028},         // badGoodIndxs_High
            {(int)confStates.SET_VECTOR_2, 0xFF20002C},         // badGoodIndxs_Low
            {(int)confStates.SET_VECTOR_3, 0xFF248000},         // 128Bins_Final
            {(int)confStates.SET_VECTOR_4, 0xFF248200},         // 128Bins_Final
            {(int)confStates.SET_VECTOR_5, 0xFF340000},         // blackmanHarris_DEC
            {(int)confStates.SET_VECTOR_6, 0xFF346000},         // 2kWin
            {(int)confStates.SET_OT_DELAY, 0xFF20003C}
        };
        public int lastOTdelay = 0;

        private async Task cofigdeviceAsync()
        {
            string Error = "";
            while (confState != (int)confStates.DONE)
            {
                string stateName = Enum.GetName(typeof(confStates), confState);
                switch (confState)
                {
                    case (int)confStates.IDLE:
                        LogMessage("Start devive configuring");
                        deviceState.Text = "Configuring device";
                        this.Refresh();
                        confState++;
                        break;

                    case (int)confStates.STOP_OT:
                        LogMessage("Configuring: Stop OT");
                        if (debugmodeEnabled)
                            deviceState.Text = "Configuring: Stop OT";
                        Error = SendStopCmd();
                        if (Error.Length > 0)
                        {
                            LogMessage("Configuring Error: " + Error);
                            MessageBox.Show("Error sending Stop OT:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        confState++;
                        this.Refresh();
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
                        uint rangeMult = 0x00000404;
                        if (appSetting.sensitivity == 1)
                            rangeMult = 0x00000101;
                        Error = WriteRegWaitResp(WriteRegs[(int)confStates.SET_RANGE], new List<uint> { rangeMult });
                        if (Error.Length > 0)
                        {
                            LogMessage("Configuring Error: " + Error);
                            MessageBox.Show("Error sending Set Range:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        confState++;
                        break;

                    case (int)confStates.SET_SPUR:
                        LogMessage("Configuring: Spurs & NN filter");
                        if (debugmodeEnabled)
                            deviceState.Text = "Configuring: Spurs & NN filter";
                        this.Refresh();
                        uint filt = 0x20023C78;
                        if (appSetting.sensitivity == 1)
                            filt = 0x00003C78;
                        Error = WriteRegWaitResp(WriteRegs[(int)confStates.SET_SPUR], new List<uint> { filt });
                        if (Error.Length > 0)
                        {
                            LogMessage("Configuring Error: " + Error);
                            MessageBox.Show("Error sending Spurs & NN filter:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                            MessageBox.Show("Error sending AWG waveform:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        confState++;
                        break;

                    case (int)confStates.SET_DAC_CONFIG:
                        LogMessage("Configuring: SET_DAC_CONFIG");
                        if (debugmodeEnabled)
                            deviceState.Text = "Configuring: SET_DAC_CONFIG";
                        this.Refresh();
                        Error = ConfigDACs();
                        if (Error.Length > 0)
                        {
                            LogMessage("Configuring Error: " + Error);
                            MessageBox.Show("Error sending SET_CHIRP_GAIN:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                            MessageBox.Show("Error sending SET_CHIRP_GAIN:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                            MessageBox.Show("Error sending SET_PM1_CONTROL:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                            MessageBox.Show("Error sending SET_PM2_CONTROL:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        confState++;
                        break;

                    case (int)confStates.SET_SOA_EN:
                        LogMessage("Configuring: Enable SOA");
                        if (debugmodeEnabled)
                            deviceState.Text = "Configuring: Enable SOA";
                        this.Refresh();

                        Error = SPISOAControl(2);
                        if (Error.Length > 0)
                        {
                            LogMessage("Configuring Error: " + Error);
                            MessageBox.Show("Error sending Enable SOA:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                            MessageBox.Show("Error sending SET_LO:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                            MessageBox.Show("Error sending SET_TX_SOA1:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                            MessageBox.Show("Error sending SET_TX_SOA2:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                            MessageBox.Show("Error sending SET_TX3_0_9:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                            MessageBox.Show("Error sending SET_TX3_10_19:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                            MessageBox.Show("Error sending SET_TX3_20_29:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                            MessageBox.Show("Error sending SET_TX3_30_39:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        lastOTdelay = deviceParameters[mode];
                        uint iotd = (uint)Math.Abs(lastOTdelay);
                        if (lastOTdelay < 0)
                            iotd = (uint)(nPoints - iotd);
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
                        // Close and reopen client due to left over messages in buffer
                        await ConnectNow();
                        optoStat.Visible = false;
                        this.Refresh();
                        if (Error.Length > 0)
                        {
                            LogMessage("Configuring Error: " + Error);
                            MessageBox.Show("Error sending Run opto:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        confState++;
                        break;

                    case (int)confStates.DONE:
                        break;

                    default:
                        MessageBox.Show("Configuration is already in progress.", "Configuration In Progress", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                }
            }
        }
        private string ConfigDACs()
        {
            string Error = "";
            //Error = WriteI2CWaitResp(3, 0x48, 0x14, 0x1F, new List<uint> { (uint)0x3F9 });
            //if (Error.Length > 0)
            //    return Error;
            //Error = WriteI2CWaitResp(3, 0x4A, 0x14, 0x1F, new List<uint> { (uint)0x3F9 });
            //if (Error.Length > 0)
            //    return Error;
            //Error = WriteI2CWaitResp(3, 0x4B, 0x14, 0x1F, new List<uint> { (uint)0x3F9 });
            //if (Error.Length > 0)
            //    return Error;
            Error = WriteI2CWaitResp(7, 0x48, 0x14, 0x1F, new List<uint> { (uint)0x3F9 });
            if (Error.Length > 0)
                return Error;
            Error = WriteI2CWaitResp(7, 0x49, 0x14, 0x1F, new List<uint> { (uint)0x3F9 });
            if (Error.Length > 0)
                return Error;
            Error = WriteI2CWaitResp(7, 0x4A, 0x14, 0x1F, new List<uint> { (uint)0x3F9 });
            if (Error.Length > 0)
                return Error;
            Error = WriteI2CWaitResp(7, 0x4B, 0x14, 0x1F, new List<uint> { (uint)0x3F9 });
            if (Error.Length > 0)
                return Error;

            return "";
        }

    }
    public enum confStates
    {
        IDLE,
        STOP_OT,
        RESET_DSP,
        SET_SENSITIVITY,
        SEND_CAPTURE_DELAY,
        SET_RANGE,
        SET_SPUR,
        SET_RETRO_LEVEL,
        SET_CHIRP_WAVEFORM,
        SET_DAC_CONFIG,
        SET_CHIRP_GAIN,
        SET_PM_CONTROL,
        SET_SOA_EN,
        LOAD_SSH_DRIVER,
        SET_LO,
        SET_TX_SOA1,
        SET_TX_SOA2,
        SET_TX3_0_9,
        SET_TX3_10_19,
        SET_TX3_20_29,
        SET_TX3_30_39,
        SET_VECTOR_1,
        SET_VECTOR_2,
        SET_VECTOR_3,
        SET_VECTOR_4,
        SET_VECTOR_5,
        SET_VECTOR_6,
        LOAD_FILES,
        SET_OT_DELAY,
        RUN_OPTOTUNE_CALIBRATION,
//        RESET_DSP,
//        SET_SENSITIVITY,
        DONE
    }
}
