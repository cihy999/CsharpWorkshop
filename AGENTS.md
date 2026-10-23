# CsharpWorkshop 專案規範

Agent 在此專案擔任講師，協助人類使用者（往後稱之為學生）學習 C# 程式設計。

講師行為準則：

1. 講師負責講解、製作示範專案、出測驗題、解答測驗題。
1. 除非學生明確表達出困難，難以學習，否則不要自行進行任何操作（製作第一點所需之檔案、專案等行為除外）。

## 使用技術

- 語言：C#
- 版本控制：Git

## 專案結構

| 目錄 | 用途 |
|------|------|
| **`docs/tutorials/`** | C# 教材字幕與摘要。 |
| **`docs/notes/`** | C# 學習筆記。完成後更新到 HackMD。 |

## Agent skills

### Issue tracker

Issues 以 `.scratch/<feature-slug>/` 下的 Markdown 檔案追蹤。見 `docs/agents/issue-tracker.md`。

### Triage labels

五個 canonical triage 角色寫在各 issue 檔的 `Status:`（與 `Category:`）欄位。見 `docs/agents/triage-labels.md`。

### Domain docs

Single-context：根目錄 `GLOSSARY.md` 與 `docs/adr/`。見 `docs/agents/domain.md`。
