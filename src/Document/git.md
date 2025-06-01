
## 作業概要

1. ブランチ一覧を確認する
2. 開発するブランチを作成/決定する
1. ブランチを開く
1. 

## ブランチ作成

git checkout main				# mainブランチに切り替え
git pull origin main            # 最新のmainブランチを取得
git checkout -b develop			# developブランチを作成
git push -u origin develop      # 今のブランチとリモートのdevelopブランチを紐づけ

# コミットのプレフィックス

feat: ユーザー登録画面を作成
fix: ログイン時にクラッシュする問題を修正
refactor: PrintEntityの責務を分離
test: ユニットテストを追加
docs: READMEに起動手順を追記
chore: .gitignoreを整理

# 1. 安定ブランチへ切り替え（developなど）
git checkout develop

# 2. 最新状態を取得（他の人の変更も取り込む）
git pull origin develop

# 3. 作業用ブランチを新規作成（わかりやすい名前で）
git checkout -b feature/print-path-resolver

# 4. コード修正・コミット
git add .
git commit -m "refactor: パス構成の責務をInfrastructure層に分離"

# 5. GitHubにプッシュ（初回は-uつける）
git push -u origin feature/print-path-resolver

# 6. PR作成（GitHub Web または CLI）
gh pr create --base develop --head feature/print-path-resolver --web
