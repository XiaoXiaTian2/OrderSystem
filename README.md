# OrderSystem

## 待改進

1. **`AI/AIAgent.cs` 的 `requestModel` 是 static**：`protected static AIRequestModel requestModel = new AIRequestModel();`，而 `DiscountContext` 每次都 `new AIAgent()` 再 `AddPrompt` 兩次。因為欄位是 static，`contents` 不會隨新的 `AIAgent` 重置，AI 模式下每次改數量都會把整份菜單 JSON 和新的點餐內容再疊上去，請求越來越大，前幾次的點餐內容也可能干擾模型判斷。修正方向：改成 instance 欄位，或在每次請求前清空 `contents`。
2. **`AIAgent.ExecRequest` 每次請求都 `new HttpClient()`**：`await (new HttpClient()).SendAsync(request)` 既沒有重用也沒有 Dispose。AI 模式下每改一次數量就會發一次請求，頻繁建立 `HttpClient` 容易耗盡 socket。檔案裡其實已經有註解掉的 `private static readonly HttpClient _httpClient`，改回共用同一個實例即可。
3. **`DiscountOrderFactory` 只是空殼**：`CreateDiscountOrder` 裡的 switch 全部被註解，永遠回傳 `null`，而且所有呼叫它的地方（`Discount.cs`、`Order.cs`）也都是註解。它和 `DiscountOrders/` 底下覆寫 `CalDisount` 的類別都是改成 Strategy 之前的遺留程式，留著會讓人誤以為還有另一條折扣流程。修正方向：刪除，或搬到另一個分支保存。
4. **手動選折扣時理由欄位會顯示 `abc`**：`DiscountContext.HandleDiscount` 非 AI 分支回傳 `("abc", "")`，這個值經過 `ShowPanel` 放進 `FullSidePanel.reason`，最後在 `Form1.GetOrder` 被寫進 `textBox1.Text`。看起來是開發時的佔位字串，應改成空字串或折扣名稱。
