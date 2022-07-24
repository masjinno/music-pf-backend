# music-pf-backend
音楽プラットフォームのバックエンド用リポジトリ

# API仕様

* 置き場：docs/api
* [Swagger Editor](https://editor.swagger.io/)で開くこと
  * VSCodeの拡張機能で閲覧などでも可
* API仕様のファイルを新規に起こすときは、 `music-pf-api_{APIの分類}.yaml` のファイル名にすること
  * 後述するAPI Gatewayに対応した各API仕様ファイル出力のため
* AWSのAPI GatewayがSwaggerのインポートをサポートしている
  * Swagger表記の一部記法が存在するとAPI Gatewayにインポートできない。以下の手順に基づきインポートすること。
    1. export_awsgateway.shを /docs/api 上で実行する
    1. API Gatewayに対応した各API仕様ファイルが出力される（プレフィックス `aws-apigateway_` が付く）
    1. AWS GatewayのAPI作成時に、出力したAPI仕様ファイルをインポートする
  * 生成されたインポート用ファイルはGit管理しないこと（.gitignore登録済み）
* APIを定義しているファイルの一覧は以下の通り

|ファイル名 |APIの用途 |
|:-- |:-- |
|music-pf-api_music.yaml |楽曲に関するAPI |

# DB構成(ER図)

* 置き場：docs/database

|ファイル名 |説明 |
|:-- |:-- |
|music-er.pu |楽曲に関するDB構成 |
