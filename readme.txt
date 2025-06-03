リポジトリ概要
--------------
- **言語/環境**: 主に C# (.NET) を利用しています。ソリューション `Architecture.sln` に複数のプロジェクトが登録されています。
- **レイヤー構造**:
  - `src/Domain`: ドメイン層。エンティティやインターフェイスを定義します。
  - `src/Application`: ユースケースやオーケストレータを実装します。
  - `src/Infrastructure`: 外部サービスとの連携を担います。
  - `src/Ui`: コンソールアプリや Web API などの実行環境です。
- **主なドキュメント**:
  - `src/Ui/PrintGenerater/ARCHITECTURE.md` に PrintGenerater のクラス構成と処理フローを記載。
  - `src/Document/git.md` に Git 運用ルールがまとめられています。

新規参加者への学習の流れ
--------------------------
1. `src/Ui/PrintGenerater/ARCHITECTURE.md` を読んで PrintGenerater の処理を理解する。
2. `src/Application/UseCases` 以下のコードを確認し、ユースケースの実装を把握する。
3. `src/Infrastructure` 配下のライブラリを確認し、外部サービスとの接続方法を学ぶ。
4. `src/Document/git.md` に従って Git のブランチ戦略やコミット規則を確認する。
5. `Architecture.sln` または各 `*.csproj` を開いて、プロジェクト間の依存関係を整理する。
