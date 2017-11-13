
namespace WorldRpgModel
{
	public class BossDropOut : DataHelper.Model.Common
    {
        public string _BossKey;

        public string _EquipKey;

        public double _Chance;

        public int _DropType;

        public string BossKey
        {

            get { return this._BossKey; }
            set { Changed = MarkChange;this._BossKey = value; }
        }
        public string EquipKey
        {

            get { return this._EquipKey; }
            set { Changed = MarkChange;this._EquipKey = value; }
        }


        public double Chance { get { return _Chance; } set { Changed = MarkChange;this._Chance = value; } }

		public int DropType
        { get { return _DropType; } set { Changed = MarkChange;this._DropType = value; } }

		public BossDropOut()
		{
			
			
		}
	}
}
