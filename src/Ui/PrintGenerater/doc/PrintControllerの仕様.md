C:\drive\work\solution\PrintSiteBuilder2\PrintGenerater\Controller\PrintController.cs

# 概要

- ユーザーからの PrintId と実行オプションを受け取り、教材印刷に関連する各種処理を制御する
- Executeコマンドで、1段階目はPrintIdを、2段階目は実行オプションを選択する。実行後は実行オプションの選択へ戻る形でループする
- 実行終了時にコンソールウィンドウを前面に表示する

# 実行オプション

| オプション | 処理内容 |
|------------|----------|
| setup      | ワークシート初期化（テンプレート削除・作成・選択） |
| qa         | QAシート・サンプルスライドのオープン |
| tex        | TeX生成（削除→生成→PDF変換→SVG変換） |
| html       | HTMLファイル生成 |
| upload     | FTPアップロード処理 |
| webpage    | Webページ（スプレッドシート）を開く |
| pdf        | PDF出力に必要な素材生成・PDF連結 |
| cover      | 商品画像（Amazon / Etsy / ECベース）生成 |
| exit       | 実行ループの終了（コンソール終了） |
