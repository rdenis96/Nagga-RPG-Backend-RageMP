using System;

namespace Domain.Models.Characters
{
    public class Skin : IEquatable<Skin>
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
        public int Sundamage { get; set; }
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

        public override bool Equals(object obj)
        {
            return Equals(obj as Skin);
        }

        public bool Equals(Skin other)
        {
            return other != null &&
                   Id == other.Id &&
                   FirstHeadShape == other.FirstHeadShape &&
                   SecondHeadShape == other.SecondHeadShape &&
                   FirstSkinTone == other.FirstSkinTone &&
                   SecondSkinTone == other.SecondSkinTone &&
                   HeadMix == other.HeadMix &&
                   SkinMix == other.SkinMix &&
                   Hair == other.Hair &&
                   FirstHairColor == other.FirstHairColor &&
                   SecondHairColor == other.SecondHairColor &&
                   Beard == other.Beard &&
                   BeardColor == other.BeardColor &&
                   Chest == other.Chest &&
                   ChestColor == other.ChestColor &&
                   Blemishes == other.Blemishes &&
                   Ageing == other.Ageing &&
                   Complexion == other.Complexion &&
                   Sundamage == other.Sundamage &&
                   Freckles == other.Freckles &&
                   EyesColor == other.EyesColor &&
                   Eyebrows == other.Eyebrows &&
                   EyebrowsColor == other.EyebrowsColor &&
                   Makeup == other.Makeup &&
                   Blush == other.Blush &&
                   BlushColor == other.BlushColor &&
                   Lipstick == other.Lipstick &&
                   LipstickColor == other.LipstickColor &&
                   NoseWidth == other.NoseWidth &&
                   NoseHeight == other.NoseHeight &&
                   NoseLength == other.NoseLength &&
                   NoseBridge == other.NoseBridge &&
                   NoseTip == other.NoseTip &&
                   NoseShift == other.NoseShift &&
                   BrowHeight == other.BrowHeight &&
                   BrowWidth == other.BrowWidth &&
                   CheekboneHeight == other.CheekboneHeight &&
                   CheekboneWidth == other.CheekboneWidth &&
                   CheeksWidth == other.CheeksWidth &&
                   Eyes == other.Eyes &&
                   Lips == other.Lips &&
                   JawWidth == other.JawWidth &&
                   JawHeight == other.JawHeight &&
                   ChinLength == other.ChinLength &&
                   ChinPosition == other.ChinPosition &&
                   ChinWidth == other.ChinWidth &&
                   ChinShape == other.ChinShape &&
                   NeckWidth == other.NeckWidth;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Id);
            hash.Add(FirstHeadShape);
            hash.Add(SecondHeadShape);
            hash.Add(FirstSkinTone);
            hash.Add(SecondSkinTone);
            hash.Add(HeadMix);
            hash.Add(SkinMix);
            hash.Add(Hair);
            hash.Add(FirstHairColor);
            hash.Add(SecondHairColor);
            hash.Add(Beard);
            hash.Add(BeardColor);
            hash.Add(Chest);
            hash.Add(ChestColor);
            hash.Add(Blemishes);
            hash.Add(Ageing);
            hash.Add(Complexion);
            hash.Add(Sundamage);
            hash.Add(Freckles);
            hash.Add(EyesColor);
            hash.Add(Eyebrows);
            hash.Add(EyebrowsColor);
            hash.Add(Makeup);
            hash.Add(Blush);
            hash.Add(BlushColor);
            hash.Add(Lipstick);
            hash.Add(LipstickColor);
            hash.Add(NoseWidth);
            hash.Add(NoseHeight);
            hash.Add(NoseLength);
            hash.Add(NoseBridge);
            hash.Add(NoseTip);
            hash.Add(NoseShift);
            hash.Add(BrowHeight);
            hash.Add(BrowWidth);
            hash.Add(CheekboneHeight);
            hash.Add(CheekboneWidth);
            hash.Add(CheeksWidth);
            hash.Add(Eyes);
            hash.Add(Lips);
            hash.Add(JawWidth);
            hash.Add(JawHeight);
            hash.Add(ChinLength);
            hash.Add(ChinPosition);
            hash.Add(ChinWidth);
            hash.Add(ChinShape);
            hash.Add(NeckWidth);
            return hash.ToHashCode();
        }
    }
}
