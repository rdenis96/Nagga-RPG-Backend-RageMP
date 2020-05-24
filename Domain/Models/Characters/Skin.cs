using GTANetworkAPI;

namespace Domain.Models.Characters
{
    public class Skin : Script
    {
        public int Id { get; set; }
        public int FirstHeadShape { get; set; }
        public int SecondHeadShape { get; set; }

        public int FirstSkinTone { get; set; }
        public int SecondSkinTone { get; set; }

        public float HeadMix { get; set; }
        public float SkinMix { get; set; }

        public int Hair { get; set; }
        public int FirstHairColor { get; set; }
        public int SecondHairColor { get; set; }

        public int Beard { get; set; }
        public int BeardColor { get; set; }

        public int Chest { get; set; }
        public int ChestColor { get; set; }

        public int Blemishes { get; set; }
        public int Ageing { get; set; }
        public int Complexion { get; set; }
        public int sundamage { get; set; }
        public int Freckles { get; set; }

        public int EyesColor { get; set; }
        public int Eyebrows { get; set; }
        public int EyebrowsColor { get; set; }

        public int Makeup { get; set; }
        public int Blush { get; set; }
        public int BlushColor { get; set; }
        public int Lipstick { get; set; }
        public int LipstickColor { get; set; }

        public float NoseWidth { get; set; }
        public float NoseHeight { get; set; }
        public float NoseLength { get; set; }
        public float NoseBridge { get; set; }
        public float NoseTip { get; set; }
        public float NoseShift { get; set; }
        public float BrowHeight { get; set; }
        public float BrowWidth { get; set; }
        public float CheekboneHeight { get; set; }
        public float CheekboneWidth { get; set; }
        public float CheeksWidth { get; set; }
        public float Eyes { get; set; }
        public float Lips { get; set; }
        public float JawWidth { get; set; }
        public float JawHeight { get; set; }
        public float ChinLength { get; set; }
        public float ChinPosition { get; set; }
        public float ChinWidth { get; set; }
        public float ChinShape { get; set; }
        public float NeckWidth { get; set; }
    }
}
