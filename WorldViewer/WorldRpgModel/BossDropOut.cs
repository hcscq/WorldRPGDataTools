
namespace WorldRpgModel
{
	public class BossDropOut
	{
		public string Key
		{

            get;set;
		}

        public string BossKey
        {

            get; set;
        }
        public string EquipKey
        {

            get; set;
        }


        public double Chance { get; set; }

		public int DropType
        { get; set; }

		public BossDropOut()
		{
			
			
		}
	}
}
