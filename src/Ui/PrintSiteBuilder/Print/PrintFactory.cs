using PrintSiteBuilder.Print.日本語.アーカイブ;
using PrintSiteBuilder.Print.日本語.かけ算;
using PrintSiteBuilder.Print.日本語.九九;
using PrintSiteBuilder.Print.日本語.割り算;
using PrintSiteBuilder.Print.日本語.引き算;
using PrintSiteBuilder.Print.日本語.数の性質;
using PrintSiteBuilder.Print.日本語.整数の性質;
using PrintSiteBuilder.Print.日本語.足し算;
using PrintSiteBuilder.Print.英語.足し算;

namespace PrintSiteBuilder.Print
{
    public static class PrintFactory
    {
    public static readonly Dictionary<string, Type> ClassNameWithClass = new Dictionary<string, Type>
    {
        { "整数の性質_倍数と約数",typeof(整数の性質_倍数と約数) },
        { "整数の性質_素因数分解",typeof(整数の性質_素因数分解) },
        { "整数の性質_累乗",typeof(整数の性質_累乗) },
        { "整数の性質_素数",typeof(素数) },
        { "文章題_2桁の足し算_30枚",typeof(文章題_二桁の足し算_30枚) },
        { "文章題_2桁の足し算_10枚",typeof(文章題_二桁の足し算_10枚) },
        { "Vertical_Addition_1Digit_1Digit_30",typeof(Vertical_Addition_1Digit_1Digit_30) },
        { "Vertical_Addition_1Digit_1Digit_10",typeof(Vertical_Addition_1Digit_1Digit_10) },
        { "Single_Digit_Addition_Without_Carrying_10",typeof(Single_Digit_Addition_Without_Regrouping_10) },
        { "Single_Digit_Addition_Without_Carrying_30",typeof(Single_Digit_Addition_Without_Regrouping_30) },
        { "Single_Digit_Addition_Without_Carrying_50",typeof(Single_Digit_Addition_Without_Regrouping_50) },
        { "Single_Digit_Addition_With_Carrying_50",typeof(Single_Digit_Addition_With_Regrouping_50) },
        { "Single_Digit_Addition_With_Carrying_30",typeof(Single_Digit_Addition_With_Regrouping_30) },
        { "Single_Digit_Addition_With_Carrying_10",typeof(Single_Digit_Addition_With_Regrouping_10) },
        { "Single_Digit_Addition_Worksheets_50",typeof(Single_Digit_Addition_Worksheets_50) },
        { "Single_Digit_Addition_Worksheets_10",typeof(Single_Digit_Addition_Worksheets_10) },
        { "Single_Digit_Addition_Worksheets_30",typeof(Single_Digit_Addition_Worksheets_30) },
        { "文章題_1桁の足し算_30枚",typeof(文章題_一桁の足し算_30枚) },
        { "文章題_1桁の足し算_10枚",typeof(文章題_一桁の足し算_10枚) },
        { "文章題_10までの足し算_30枚",typeof(文章題_10までの足し算_30枚) },
        { "文章題_10までの足し算_10枚",typeof(文章題_10までの足し算_10枚) },    
        {  "一桁の足し算 虫食い算 30枚", typeof(一桁の足し算_虫食い算_30枚) },
        {  "一桁の足し算 虫食い算 10枚", typeof(一桁の足し算_虫食い算_10枚) },
        {  "筆算 10桁の足し算 30枚", typeof(筆算_十桁の足し算_30枚) },
        {  "筆算 10桁の足し算 10枚", typeof(筆算_十桁の足し算_10枚) },
        {  "筆算 4桁の足し算 30枚", typeof(筆算_四桁の足し算_30枚) },
        {  "筆算 4桁の足し算 10枚", typeof(筆算_四桁の足し算_10枚) },
        {  "筆算 3桁の足し算 10枚", typeof(筆算_三桁の足し算_10枚) },
        {  "筆算 3桁の足し算 30枚", typeof(筆算_三桁の足し算_30枚) },
        {  "筆算 2桁の足し算 10枚", typeof(筆算_二桁の足し算_10枚) },
        {  "筆算 2桁の足し算 30枚", typeof(筆算_二桁の足し算_30枚) },
        {  "筆算 2桁+1桁の足し算 10枚", typeof(筆算_二桁_一桁の足し算_10枚) },
        {  "筆算 2桁+1桁の足し算 30枚", typeof(筆算_二桁_一桁の足し算_30枚) },
        {  "筆算 一桁の足し算 10枚", typeof(筆算_一桁の足し算_10枚) },
        {  "筆算 一桁の足し算 30枚", typeof(筆算_一桁の足し算_30枚) },
        { "一桁の足し算 繰り上がり無 30枚", typeof(一桁の足し算_繰り上がり無_30枚) },
        { "一桁の足し算 繰り上がり無 10枚", typeof(一桁の足し算_繰り上がり無_10枚) },
        { "一桁の足し算 繰り上がり有 30枚", typeof(一桁の足し算_繰り上がり有_30枚) },
        { "一桁の足し算 繰り上がり有 10枚", typeof(Single_Digit_Addition_With_Regrouping_10) },
        { "一桁の足し算 ランダム 10枚", typeof(一桁の足し算_ランダム_10枚) },
        { "一桁の足し算 ランダム 30枚", typeof(一桁の足し算_ランダム_30枚) },
        { "足し算(たす1)", typeof(足し算_たす1) },
        { "足し算(たす2)", typeof(足し算_たす2) },
        { "足し算(たす3)", typeof(足し算_たす3) },
        { "足し算(たす3まで)", typeof(足し算_たす3まで) },
        { "足し算(たす4)", typeof(足し算_たす4) },
        { "足し算(たす5)", typeof(足し算_たす5) },
        { "足し算(たす5まで)", typeof(足し算_たす5まで) },
        { "足し算(たす6)", typeof(足し算_たす6) },
        { "足し算(たす7)", typeof(足し算_たす7) },
        { "足し算(たす8)", typeof(足し算_たす8) },
        { "足し算(たす9)", typeof(足し算_たす9) },
        { "足し算(たす10)", typeof(足し算_たす10) },
        { "足し算(たす10まで)", typeof(足し算_たす10まで) },
        { "足し算(たす11)", typeof(足し算_たす11) },
        { "足し算(たす12)", typeof(足し算_たす12) },
        { "足し算(たす13)", typeof(足し算_たす13) },
        { "足し算(たす14)", typeof(足し算_たす14) },
        { "足し算(たす15)", typeof(足し算_たす15) },
        { "足し算(たす16)", typeof(足し算_たす16) },
        { "足し算(たす17)", typeof(足し算_たす17) },
        { "足し算(たす18)", typeof(足し算_たす18) },
        { "足し算(たす19)", typeof(足し算_たす19) },
        { "足し算(たす20)", typeof(足し算_たす20) },
        { "約数", typeof(約数) },
        { "公約数・最大公約数", typeof(公約数_最大公約数) },
        { "公約数の文章題", typeof(公約数の文章題) },
        { "倍数", typeof(倍数) },
        { "公倍数・最小公倍数", typeof(公倍数_最小公倍数) },
        { "四つの分数の割り算", typeof(四つの分数の割り算) },
        { "三つの分数の割り算", typeof(三つの分数の割り算) },
        { "二つの分数の割り算", typeof(二つの分数の割り算) },
        { "分数の割り算テンプレ", typeof(分数の割り算テンプレ) },
        { "一桁の引き算", typeof(一桁の引き算) },
        {  "20までの引き算(繰り下がり無)", typeof(二十までの引き算_繰り下がり無) },
        {  "20までの引き算(繰り下がり有)", typeof(二十までの引き算_繰り下がり有) },
        {  "引き算(ひく1)", typeof(引き算_ひく1) },
        {  "引き算(ひく2)", typeof(引き算_ひく2) },
        {  "引き算(ひく3)", typeof(引き算_ひく3) },
        {  "引き算(ひく3まで)", typeof(引き算_ひく3まで) },
        {  "引き算(ひく5まで)", typeof(引き算_ひく5まで) },
        {  "引き算(ひく10まで)", typeof(引き算_ひく10まで) },
        {  "一桁の足し算(100マス計算)", typeof(一桁の足し算_100マス計算) },
        {  "20までの引き算(100マス計算)", typeof(二十までの引き算_100マス計算) },
        {  "九九(1の段)", typeof(九九_1の段) },
        {  "九九(2の段)", typeof(九九_2の段) },
        {  "九九(3の段)", typeof(九九_3の段) },
        {  "九九(4の段)", typeof(九九_4の段) },
        {  "九九(5の段)", typeof(九九_5の段) },
        {  "九九(6の段)", typeof(九九_6の段) },
        {  "九九(7の段)", typeof(九九_7の段) },
        {  "九九(8の段)", typeof(九九_8の段) },
        {  "九九(9の段)", typeof(九九_9の段) },
        {  "2桁と1桁のかけ算", typeof(二桁と一桁のかけ算) },
        {  "3桁と1桁のかけ算", typeof(三桁と一桁のかけ算) },
        {  "4桁と1桁のかけ算", typeof(四桁と一桁のかけ算) },
    };

        public static IPrint GetPrintClass(string type)
        {
            if (ClassNameWithClass.TryGetValue(type, out var contentType))
            {
                return (IPrint)Activator.CreateInstance(contentType);
            }
            throw new ArgumentException("PrintFactoryにクラスを追加していません。");
        }
    }
}
