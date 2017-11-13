using System.Runtime.CompilerServices;

namespace WorldRpgModel
{
	public class Material : DataHelper.Model.Unit
    {


        public string _Boss;

        public string Boss
        { get { return this._Boss; } set { Changed = MarkChange;this._Boss = value; } }

        public Material()
		{
			
			
		}
	}
}
