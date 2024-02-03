Azure Functionsを使用してHttp Clientを使ってBlob Storageの読み書きができた、Unityから使える、SDKも不要

Azure Blob Storage　をhttp APIでクライアントに公開したい
ChatGPT

Azure Blob StorageをHTTP APIを通してクライアントに公開する方法はいくつかあります。最も一般的な方法は、以下の手順に従って設定することです：

Azure Blob Storageの設定:

Azureポータルにサインインします。
新しいストレージアカウントを作成するか、既存のアカウントを使用します。
Blobサービス内にコンテナを作成し、アクセスポリシー（プライベート、Blob公開、またはコンテナ公開）を設定します。
共有アクセス署名 (SAS) の生成:

特定のBlobまたはコンテナへの限定的なアクセス権を持つURLを生成するために、共有アクセス署名（SAS）を使用できます。
SASは、ストレージアカウントのキーを共有せずに、特定の期間、IPアドレス範囲、または特定の操作（読み取り、書き込み、削除）に対するアクセスを許可するためのものです。
Azure AD 認証の使用:

よりセキュアなアプローチとして、Azure Active Directory (AD) 認証を使用してアクセスを制御できます。
クライアントは、Azure ADからトークンを取得し、そのトークンを使用して認証されたリクエストをBlobサービスに送信します。
Azure Functions または Azure API Managementの使用:

よりカスタマイズされたロジックや認証が必要な場合、Azure Functionsを使用してAPIエンドポイントを作成し、そこからBlob Storageにアクセスすることができます。
Azure API Managementを使用してAPIを公開し、レート制限、アクセスポリシー、または変換ロジックを追加することもできます。
CORS (Cross-Origin Resource Sharing) の設定:

Webアプリケーションから直接Blob Storageにアクセスする場合は、CORS設定を適切に設定して、異なるオリジンからの安全なリクエストを許可する必要があります。
これらのステップに従って、Azure Blob StorageのデータをHTTP APIを通じてクライアントに安全に公開できます。各ステップにはAzureポータル、Azure CLI、またはAzure SDKを使用できます。安全性の観点から、最小限の権限を持つアクセスポリシーを使用し、必要な場合は適切な認証と承認メカニズムを実装することが重要です。



