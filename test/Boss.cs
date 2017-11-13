
namespace WorldRpgModel
{
	public class Boss:DataHelper.Model.Common
	{
        private string _Name;
        private string _Beckon;
        private string _DropOut;
        public string Beckon { get; set; }
        public string DropOut { get; set; }
        public string Name { get { return this._Name; } set {  Changed=MarkChange;this._Name = value; } }


        //public string Beckon { get { return this.Beckon; } set { this.Changed = MarkChange; this.Beckon = value; } }

        //public string DropOut
        //      { get { return this.DropOut; } set { this.Changed = MarkChange; this.DropOut = value; } }

	}
}
