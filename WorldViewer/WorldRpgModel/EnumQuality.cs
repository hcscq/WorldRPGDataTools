using System.Drawing;

namespace WorldRpgModel
{
    public enum EnumQuality
    {
        普通,
        魔法,
        罕见,
        极其罕见,
        天绝史诗,
        传奇至宝,
        神话传说,
        禁断圣物
    }
    public struct TagDropType
    {
        public int Index;
        public string Name;
        public Color TextColor; 
        public TagDropType(int index,string name,Color color) { Index = index;Name = name;TextColor = color; }
        public TagDropType(int index, string name) { Index = index; Name = name; TextColor = Color.Black; }
    }
    public static class DropType{
        public static TagDropType Drop = new TagDropType(0,"掉落");
        public static TagDropType Dig = new TagDropType(1, "挖取");
        public static TagDropType OpenBox = new TagDropType(2, "开箱");
        public static TagDropType Undefined = new TagDropType(-1, "未定义");
        public static string GetDropTypeName(int typeId) { return Drop.Index == typeId ? Drop.Name : (Dig.Index == typeId ? Dig.Name : (OpenBox.Index == typeId ? OpenBox.Name : (Undefined.Name))); }
    }
    public struct TagLabelTag
    {
        public const string Name = "Name";
        public const string Chance = "Chance";
        public const string DropType = "DropType";
    }
}
