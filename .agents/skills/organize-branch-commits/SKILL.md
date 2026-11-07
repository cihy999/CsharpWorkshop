---
name: organize-branch-commits
description: 依職責重整指定 Git 分支的本機 commit 歷史，保持最終 tree 與來源署名不變。
disable-model-invocation: true
---

# Organize Branch Commits

將分支歷史整理為「一個 commit 表達一個目的、容易審閱」的序列。Commit 數量由職責邊界決定；最終 Git tree 必須與整理前完全相同。

執行範圍限於本機歷史整理。不修改檔案內容，不執行 push；分析時發現的內容問題另行回報。

## 1. 固定重寫範圍

- 讀取目標 repository 的 `AGENTS.md` 與其他適用規範，commit 訊息格式與安全規則以其為準。
- 從使用者要求與目前上下文確定 repository、目標 branch、額外分組或訊息要求；缺少且無法唯一判定的必要輸入才詢問。
- 確認目標 branch 存在，且不是 default／protected branch。
- 確認 working tree 與 index 乾淨，且沒有進行中的 merge、rebase 或 cherry-pick；不符合時停止並回報現況。
- 使用指定的 base；未指定時由 remote default branch、分支圖與 merge-base 推斷。若合理候選會導致不同重寫範圍，請使用者決定。
- 記錄 base、merge-base、目標分支原始 HEAD SHA 與 tree SHA，後續均以固定 SHA 分析與驗證。

**完成條件**：重寫範圍唯一且符合分支限制，Git 狀態乾淨，原始 HEAD 與 tree 可供恢復及比對。

## 2. 建立職責分組

- 檢視 merge-base 到原始 HEAD 的每個 commit message、變更檔案、stat 與 patch，記錄來源署名。
- 將服務同一目的的變更合併，包含後續修正、補充與拼字調整；可獨立審閱與撤銷的變更各自成組。
- 以 hunk 與意圖判斷職責。同一檔案可跨組；互相依賴或由同一來源產生的檔案同組。
- 依依賴與邏輯順序排列，讓每個中間 commit 可獨立理解，避免明顯的中間壞狀態。
- 範圍含 merge commits 時，辨識需保留的拓撲與合併解衝突內容；若規劃壓平，先說明具體影響。
- 建立舊 commit → 新職責分組的對照。原始 commit 若跨組或其變更已被後續撤銷，明列拆分去向或抵銷原因。
- 在對話中簡述分組、順序與預計數量，再進行改寫。

**完成條件**：每個原始 commit 的變更與署名都有去向，每組有單一目的，依賴與 merge 處理方式已確定。

## 3. 改寫本機歷史

- 選擇可實現分組的 rebase、fixup／squash 或從 merge-base 重建方式；使用可恢復的操作，不使用 `git reset --hard`。
- 僅重組固定範圍內的變更，依目標 repository 規範撰寫訊息，並套用下方「來源署名」。
- Hook 失敗時處理原因並保留 hook 驗證，不使用 `--no-verify`；若修復需要改變原始最終 tree，停止並回報衝突，保留恢復資訊。

**完成條件**：目標本機分支已形成預定序列，每個新 commit 都可對應到來源分組，Git 操作已結束。

### 來源署名

歷史整理沿用原始歸屬，不新增執行 Agent 的署名：

- 每個新 commit 收納來源 commits 已有的署名 trailer（如 `Co-authored-by`、`Assisted-by`），依原始時間順序保留，去除完全重複的行。
- 來源 commit 拆到多組時，各組均保留其署名；來源沒有署名時維持無署名。
- 若環境會自動附加署名，須確認產出的訊息仍符合本規則。

## 4. 驗證與交付

- 比對新 HEAD 與原始 HEAD 的 tree SHA。不同即視為失敗，停止交付；安全恢復到原始 HEAD，或回報差異與可恢復的 SHA。
- 確認 working tree 與 index 乾淨，且沒有未完成的 Git 操作。
- 逐個檢查新 commit 的 diff、職責與來源署名，核對步驟 2 的對照，確認每項原始變更均已處理。
- 最終內容相同，預設不需額外驗證；若中間 commit 的完整性存疑，依 repository 規範執行適當檢查（如 build、測試或 lint）。
- 回報整理前後 commit 數量、新 commit SHA 與職責、舊 commit 對照、原始與新 HEAD、tree 比對及工作目錄狀態，註明驗證範圍與未 push。
- 若分支已在遠端發佈，說明後續更新遠端需要受控的 force push，留待使用者另行要求。

**完成條件**：最終 tree 完全相同、Git 狀態乾淨、所有分組與署名核對通過，且結果與驗證限制已回報。
