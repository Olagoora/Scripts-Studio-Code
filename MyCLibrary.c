#include <stdio.h>

__attribute__((dllexport)) int add_numbers(int a, int b)
{
	return a + b;
}

__attribute__((dllexport)) void fast_compute(int *data, int size)
{
	for (int i = 0; i < size; i++)
	{
		data[i] *= 2;
	}
}
