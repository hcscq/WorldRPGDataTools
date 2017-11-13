
namespace WorldRpgModel
{
	public class Boss:DataHelper.Model.Unit
	{
        private string _Beckon;
        private string _DropOut;

        public string Beckon { get { return this._Beckon; } set { this.Changed = MarkChange; this._Beckon = value; } }

        public string DropOut
        { get { return this._DropOut; } set { this.Changed = MarkChange; this._DropOut = value; } }

        public Boss()
		{
			
			
		}
	}
}
