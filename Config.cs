namespace OdemControl
{
    public partial class Form1
    {
        public List<string> ConfParams = new List<string>()
        {
            "Chirp AWG gain","PM1","PM2","LO","TxSOA1","TxSOA2","Tx3_0_9","Tx3_10_19",
            "Tx3_20_29","Tx3_30_39"
        };
        public Dictionary<string, string> ParamsList = new Dictionary<string, string>()
        {
            {"Chirp AWG gain","100"},
            {"PM1","200"},
            {"PM2","200"},
            {"LO","10"},
            {"TxSOA1","1"},
            {"TxSOA2","1"},
            {"Tx3_0_9","20"},
            {"Tx3_10_19","30"},
            {"Tx3_20_29","40"},
            {"Tx3_30_39","50" }
        };
    }
}
