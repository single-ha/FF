using LitJson;

namespace Assets.Script.Data
{
    public class SphereTemplate:DataBase
    {
        public SphereTemplate(JsonData json)
        {
            SetJson(json);
        }

        public int Level
        {
            get
            {
                return reader.ReadInt("level");
            }
        }
        public string Earth
        {
            get
            {
                return reader.ReadStr("earth");
            }
        }
        public string Terrain
        {
            get
            {
                return reader.ReadStr("terrain");
            }
        }

        private string[] buildings;
        public string[] Buildings
        {
            get
            {
                reader.ReadArray(ref buildings, "buildings");
                return buildings;
            }
        }
        private int[] buildings_x;
        public int[] Buildings_X
        {
            get
            {
                reader.ReadArray(ref buildings_x, "buildings_x");
                return buildings_x;
            }
        }
        private int[] buildings_y;
        public int[] Buildings_Y
        {
            get
            {
                reader.ReadArray(ref buildings_y, "buildings_y");
                return buildings_y;
            }
        }

        private int[] buildings_r;

        public int[] Buildings_R
        {
            get
            {
                reader.ReadArray(ref buildings_r, "buildings_r");
                return buildings_r;
            }
        }
        private string[] characters_id;

        public string[] CharactersId
        {
            get
            {
                reader.ReadArray(ref characters_id, "characters_id");
                return characters_id;
            }
        }
        private int[] characters_evo;

        public int[] CharactersEvo
        {
            get
            {
                reader.ReadArray(ref characters_evo, "characters_evo");
                return characters_evo;
            }
        }

        public string cover
        {
            get
            {
                return reader.ReadStr("cover", "3000");
            }
        }

        public string ring
        {
            get
            {
                return reader.ReadStr("ring");
            }
        }

        public string roof
        {
            get
            {
                return reader.ReadStr("roof");
            }
        }
        public string wall
        {
            get
            {
                return reader.ReadStr("wall");
            }
        }
    }
}