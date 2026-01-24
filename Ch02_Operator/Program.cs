namespace Ch02_Operator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 算數運算子
            int i, j = 20, k = 3;
            i = j + k;
			Console.WriteLine($"{i} = {j} + {k}");
			i = j - k;
			Console.WriteLine($"{i} = {j} - {k}");
			i = j * k;
			Console.WriteLine($"{i} = {j} * {k}");
			i = j / k;
			Console.WriteLine($"{i} = {j} / {k}");
			i = j % k;
			Console.WriteLine($"{i} = {j} % {k}");

			// 比較運算子
			int a = 23, b = 98;
			Console.WriteLine($"{a} > {b} => {a > b}");
			Console.WriteLine($"{a} < {b} => {a < b}");
			Console.WriteLine($"{a} >= {b} => {a >= b}");
			Console.WriteLine($"{a} <= {b} => {a <= b}");
			Console.WriteLine($"{a} == {b} => {a == b}");
			Console.WriteLine($"{a} != {b} => {a != b}");

			// 邏輯運算子
			bool l = false , r = true;
			Console.WriteLine($"{l} && {r} => {l && r}");
			Console.WriteLine($"{l} || {r} => {l || r}");
			Console.WriteLine($"!{l}, !{r} => {!l}, {!r}");

			// 位元運算子
			// n1 = |0001|, n2 =|0010|, n3 = |0011|
			int n1 = 1, n2 = 2, n3 = 3;
			Console.WriteLine($"{n1} & {n2} => {n1 & n2}");
			Console.WriteLine($"{n1} | {n2} => {n1 | n2}");
			Console.WriteLine($"{n1} ^ {n2} => {n1 ^ n2}");
			Console.WriteLine($"~{n3} => {~n3}");

			// 位移運算子
			Console.WriteLine($"1 << 1 = {1 << 1}");
			Console.WriteLine($"1 >> 1 = {1 >> 1}");

			// 複合指定運算子
			i = 5;
			i += 1;
			Console.WriteLine($"i += 1 => {i}");
			i -= 1;
			Console.WriteLine($"i -= 1 => {i}");
			i *= 2;
			Console.WriteLine($"i *= 2 => {i}");
			i /= 2;
			Console.WriteLine($"i /= 2 => {i}");
			i %= 2;
			Console.WriteLine($"i %= 2 => {i}");
			i &= 2;
			Console.WriteLine($"i &= 2 => {i}");
			i |= 2;
			Console.WriteLine($"i |= 2 => {i}");
			i ^= 2;
			Console.WriteLine($"i ^= 2 => {i}");
			i <<= 2;
			Console.WriteLine($"i <<= 2 => {i}");
			i >>= 2;
			Console.WriteLine($"i >>= 2 => {i}");

			// 遞增遞減運算子
			i = 10;
			Console.WriteLine($"i++ = {i++}");	// 先丟 i 的值，再執行 i = i + 1
			Console.WriteLine($"++i = {++i}");  // 執行 i = i + 1，再丟 i 的值
		}
    }
}
