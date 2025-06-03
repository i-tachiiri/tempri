## 作業概要

1. ブランチ一覧を確認する
2. 開発するブランチを作成/決定する
1. ブランチを切り替える
1. プルリクエストを作成する

## ブランチ作成

git checkout main			# mainブランチに切り替え
git pull origin main	# 最新のmainブランチを取得
git checkout -b develop		# developブランチを作成
git push -u origin develop	# 今のブランチとリモートのdevelopブランチを紐づけ

# コミットのプレフィックス

feat: ユーザー登録画面を作成
fix: ログイン時にクラッシュする問題を修正
refactor: PrintEntityの責務を分離
test: ユニットテストを追加
docs: READMEに起動手順を追記
chore: .gitignoreを整理

## コメント
