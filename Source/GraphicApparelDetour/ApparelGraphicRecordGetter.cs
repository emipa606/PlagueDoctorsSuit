using RimWorld;
using UnityEngine;
using Verse;

namespace GraphicApparelDetour;

internal static class ApparelGraphicRecordGetter
{
    internal static bool TryGetGraphicApparel(Apparel apparel, BodyTypeDef bodyType, out ApparelGraphicRecord rec)
    {
        var drawColor = apparel.DrawColor;
        var white = Color.white;
        if (bodyType == null)
        {
            Log.Error("Getting apparel graphic with undefined body type.");
            bodyType = BodyTypeDefOf.Male;
        }

        bool result;
        if (apparel.def.apparel.wornGraphicPath.NullOrEmpty())
        {
            rec = new ApparelGraphicRecord(null, null);
            result = false;
        }
        else
        {
            var shader = ShaderDatabase.Cutout;
            if (apparel.def.graphicData.shaderType == ShaderTypeDefOf.CutoutComplex)
            {
                shader = ShaderDatabase.CutoutComplex;
            }

            var text = apparel.def.apparel.LastLayer == ApparelLayerDefOf.Overhead
                ? apparel.def.apparel.wornGraphicPath
                : $"{apparel.def.apparel.wornGraphicPath}_{bodyType}";

            var graphic = GraphicDatabase.Get<Graphic_Multi>(text, shader, apparel.def.graphicData.drawSize,
                drawColor, white);
            rec = new ApparelGraphicRecord(graphic, apparel);
            result = true;
        }

        return result;
    }
}