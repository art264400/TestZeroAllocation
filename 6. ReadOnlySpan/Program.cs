int[] array = new int[] { 1, 2, 3, 4, 5 };

// Создание Span<int> из массива
Span<int> span = array.AsSpan();

// Изменение значения элемента в Span<int>
span[0] = 10;
Console.WriteLine($"span[0]: {span[0]}"); // Output: span[0]: 10

// Создание ReadOnlySpan<int> из массива
ReadOnlySpan<int> readOnlySpan = array.AsSpan();

// Чтение значения элемента из ReadOnlySpan<int>
Console.WriteLine($"readOnlySpan[1]: {readOnlySpan[1]}"); // Output: readOnlySpan[1]: 2

// Ошибка компиляции: ReadOnlySpan не допускает изменение значений элементов
//readOnlySpan[1] = 20;
