# 05-deferred-execution 課堂筆記

## 課堂練習的回答

### 01 / 產生端怎麼結束：走到結尾 vs yield break

產生端在 try 裡執行 yield break 時，接著會發生什麼？

答：執行清理然後結束。

### 02 / 消費端怎麼停下：foreach 裡的 break

消費端在 foreach 裡執行 break 後，迭代器主體會？

答：只執行已進入的清理。

### 03 / 誰觸發了 finally？沒開始列舉又會怎樣？

取得列舉器但從未呼叫 MoveNext() 就離開 using，主體的 finally 會？

答：不會執行。

### 04 / 預測三種結束路徑

答：

```text
Open
A1
A2
Done
Clean
After
Open
B1
Clean
Open
C1
Clean
```

## 補充：yield break 一定搭配 try-finally 嗎？

> 待 Agent 回答。
