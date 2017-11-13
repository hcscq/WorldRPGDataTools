
namespace WorldRpgModel
{
	public class Exclusive : DataHelper.Model.Common
    {
        public string _HeroKey;

        public string _EquipKey;

        public string _Name;

        public string _Effect;
        public string HeroKey
        { get { return this._HeroKey; } set { Changed = MarkChange;this._HeroKey = value; } }

        public string EquipKey
        { get { return this._EquipKey; } set { Changed = MarkChange;this._EquipKey = value; } }

        public string Name
        { get { return this._Name; } set { Changed = MarkChange;this._Name = value; } }

        public string Effect
        { get { return this._Effect; } set { Changed = MarkChange;this._Effect = value; } }

        public Exclusive()
		{
			
			
		}
	}
}
