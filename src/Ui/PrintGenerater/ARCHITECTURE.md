# PrintGenerater アーキテクチャ解説

## 1. クラス・関数の解説

### Factories

#### PrintFactory
- **コンストラクタ**: IServiceProvider, IPageService を受け取り、印刷エンティティ生成の基盤を構築
- **CreateInstanceAsync(int printId)**: 指定IDの印刷エンティティを非同期生成
- **InitializePrintAsync(IPrintEntity print)**: 印刷エンティティの初期化処理（非公開）

### Services

#### EtzyImageGenerator
- **GenerateImage(IPrintMasterEntity print)**: 画像生成
- **ReplaceAnswerImage(IPrintMasterEntity print)**: 解答画像の差し替え
- **ReplaceThumbAnswerImage(IPrintMasterEntity print)**: サムネイル解答画像の差し替え
- **ReplaceQuestionImage(IPrintMasterEntity print)**: 問題画像の差し替え
- **ExportItemPngs(IPrintMasterEntity print)**: アイテムPNG出力
- **ExportThumbPngs(IPrintMasterEntity print)**: サムネイルPNG出力

#### Pdf2PngConverter
- **Convert(IPrintMasterEntity master)**: PDF→PNG変換
- **ConvertQuestionTex(IPrintMasterEntity master)**: 問題TeXの変換
- **ConvertAnswerTex(IPrintMasterEntity master)**: 解答TeXの変換
- **Convert2Svg(string texPdfPath)**: PDF→SVG変換

#### PrintMasterGetter
- **GetPrintEntity(int printId)**: 印刷マスターエンティティ取得
- **mapPrint2Worksheet(IPrintMasterEntity print)**: 印刷→ワークシートマッピング

#### ProductImageUploader
- **UploadImages(IPrintEntity print)**: 商品画像アップロード

#### TemplateDuplicator
- **SetPrintDirectory(IPrintEntity print)**: 印刷ディレクトリ設定
- **DuplicateTemplate(string PrintId, string Language)**: テンプレート複製

## 2. 処理の流れ（概要）

1. **印刷エンティティ生成**
   - PrintFactory で印刷エンティティを生成・初期化

2. **画像・PDF生成/変換**
   - EtzyImageGenerator, Pdf2PngConverter で画像やPDFの生成・変換

3. **データ取得・マッピング**
   - PrintMasterGetter で印刷データやワークシート情報を取得

4. **アップロード・複製**
   - ProductImageUploader で画像をアップロード
   - TemplateDuplicator でテンプレートを複製

※詳細な処理や各クラスの連携は、今後さらに追記可能です。