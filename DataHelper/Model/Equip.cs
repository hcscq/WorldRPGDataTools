

namespace WorldRpgModel
{
	public class Equip : DataHelper.Model.Unit
    {

        public string _Quality;

        public string _Attribute;

        public string _Level;

        public string _Origin;

        public string _Exclusive;

        public string _Type;

		public string Quality
		{
            get { return this._Quality; }
            set { Changed = MarkChange;this._Quality = value; }
        }

		public string Attribute
		{
            get { return this._Attribute; }
            set { Changed = MarkChange;this._Attribute = value; }
        }

		public string Level
		{
            get { return this._Level; }
            set { Changed = MarkChange; this._Level = value; }
        }

		public string Origin
		{
            get { return this._Origin; }
            set { Changed = MarkChange;this._Origin = value; }
        }

		public string Exclusive
		{
            get { return this._Exclusive; }
            set { Changed = MarkChange;this._Exclusive = value; }
        }

		public string Type
		{
            get { return this._Type; }
            set { Changed = MarkChange;this._Type = value; }
        }

		public Equip()
		{
			
			
		}
	}
}
