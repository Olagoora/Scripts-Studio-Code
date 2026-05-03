#include <stdio.h>
#include <string.h>
#include <stdint.h>

// Fonction professionnelle : calcul de hash simple (MurmurHash-like)
__attribute__((dllexport)) uint32_t hash_data(const unsigned char* data, int length) {
    uint32_t hash = 2166136261u;
    for (int i = 0; i < length; i++) {
        hash ^= data[i];
        hash *= 16777619u;
    }
    return hash;
}

// Fonction professionnelle : compression simple (RLE - Run Length Encoding)
__attribute__((dllexport)) int compress_data(const unsigned char* input, int input_len, unsigned char* output, int output_max) {
    int out_pos = 0;
    int i = 0;

    while (i < input_len && out_pos < output_max - 2) {
        unsigned char current = input[i];
        int count = 1;

        while (i + count < input_len && input[i + count] == current && count < 255) {
            count++;
        }

        output[out_pos++] = (unsigned char)count;
        output[out_pos++] = current;
        i += count;
    }

    return out_pos;
}

// Fonction professionnelle : décompression
__attribute__((dllexport)) int decompress_data(const unsigned char* input, int input_len, unsigned char* output, int output_max) {
    int out_pos = 0;
    int i = 0;

    while (i < input_len - 1 && out_pos < output_max) {
        int count = input[i];
        unsigned char value = input[i + 1];

        for (int j = 0; j < count && out_pos < output_max; j++) {
            output[out_pos++] = value;
        }

        i += 2;
    }

    return out_pos;
}