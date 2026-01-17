# Antigravity設定手冊

這份文件說明各項設定文件、檔案對 Antigravity 助手的影響。

## .gitignore

當 AI 分析專案時，`.gitignore` 會是一個過濾器，告訴 AI 哪些內容是雜訊，哪些才是核心。具體影響可以歸納為以下三點：

1. **聚焦核心邏輯**: 幫助 AI 過濾編譯產生的雜訊，專注於原始碼。
2. **安全性與穩定性**: 避免 AI 修改到 IDE 設定或自動生成檔案。
3. **執行效率**: 減少掃描不必要檔案的時間，加快回應速度。

## .cursorignore 與 .cursorindexingignore

[官方文件](https://docs.cursor.com/context/ignore-files)

### .cursorignore 檔案

- 完全封鎖。AI 看不到、搜不到、也無法回答相關問題。
- 適用於：敏感資料 (API keys, 憑證)、機密文件。

### .cursorindexingignore 檔案

- AI 預設「搜不到」這些檔案，不會主動引用。
- 但如果您「手動開啟」或「明確 @ 提到」該檔案，AI 仍然可以讀取並回答。
- 適用於：大型自動生成檔、文件檔、或不想干擾全域搜尋的舊程式碼。
