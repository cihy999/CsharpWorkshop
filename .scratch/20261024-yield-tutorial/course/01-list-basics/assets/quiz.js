// 僅在本頁顯示回饋；不儲存作答、不傳送資料，也不執行輸入的 C#。
document.querySelectorAll('form[data-feedback]').forEach(form => {
    form.addEventListener('submit', event => {
        event.preventDefault();
        const output = form.querySelector('[role="status"]');
        const input = form.querySelector('textarea');
        if (input) {
            const tokens = input.value.trim().split(/[\s,，、]+/u).filter(Boolean);
            if (!tokens.length) {
                output.textContent = '請先寫下預測，再核對。';
                return;
            }
            const expected = form.dataset.expected.split(' ');
            const correct = tokens.length === expected.length && tokens.every((value, i) => value === expected[i]);
            output.textContent = (correct ? '預測一致。' : '還有差異。') + ' ' + form.dataset.feedback;
            return;
        }
        const choice = form.querySelector('input:checked');
        output.textContent = choice
            ? (choice.value === form.dataset.answer ? '答對了。' : '再想一下。') + ' ' + form.dataset.feedback
            : '請先選一個答案。';
    });
});
