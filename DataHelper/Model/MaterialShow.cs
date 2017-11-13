using System.Runtime.CompilerServices;

namespace WorldRpgModel
{
	public class MaterialShow : Material
    {

        public string BossKey
        { get { return this._Boss; } set { Changed = MarkChange;this._Boss = value; } }
        private string _BossName;
        public string BossName { get { return _BossName; } set { this._BossName = value; } }
        public MaterialShow()
		{
			
			
		}
	}
}
