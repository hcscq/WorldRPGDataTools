

namespace WorldRpgModel
{
	public class Hero : DataHelper.Model.Unit
    {


        public string _Vocation;

        public int _Type;

        public long _Hp;

        public long _Mp;

        public long _AttMin;

        public long _AttMax;

        public long _Ll;

        public long _Mj;

        public long _Zl;

        public long _Hj;

        public string Vocation
        { get { return this._Vocation; } set { Changed = MarkChange; this._Vocation = value; } }

        public int Type
        { get { return this._Type; } set { Changed = MarkChange; this._Type = value; } }

        public long Hp
        { get { return this._Hp; } set { Changed = MarkChange;this._Hp = value; } }

        public long Mp
        { get { return this._Mp; } set { Changed = MarkChange; this._Mp = value; } }

        public long AttMin
        { get { return this._AttMin; } set { Changed = MarkChange; this._AttMin = value; } }

        public long AttMax
        { get { return this._AttMax; } set { Changed = MarkChange; this._AttMax = value; } }

        public long Ll
        { get { return this._Ll; } set { Changed = MarkChange; this._Ll = value; } }

        public long Mj
        { get { return this._Mj; } set { Changed = MarkChange; this._Mj = value; } }

        public long Zl
        { get { return this._Zl; } set { Changed = MarkChange; this._Zl = value; } }

        public long Hj
        { get { return this._Hj; } set { Changed = MarkChange; this._Hj = value; } }

        public Hero()
		{
			
			
		}
	}
}
