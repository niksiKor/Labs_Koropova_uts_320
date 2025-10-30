using System;

class NetworkCalculator
{
    static void Main()
    {
        Console.WriteLine("Введите IP-адрес (формат: xxx.xxx.xxx.xxx):");
        string ipInput = Console.ReadLine();

        Console.WriteLine("Введите маску сети (формат: xxx.xxx.xxx.xxx):");
        string maskInput = Console.ReadLine();

        try
        {
            // Проверяем корректность маски  
            if (!IsValidSubnetMask(maskInput))
            {
                Console.WriteLine("Ошибка: Некорректная маска сети! Маска должна состоять из последовательности 1, за которой следуют 0.");
                return;
            }

            uint ip = IpToUint(ipInput);
            uint mask = IpToUint(maskInput);

            // Вычисление адреса сети  
            uint networkAddress = ip & mask;

            // Вычисление количества хостов  
            int hostBits = 32 - CountLeadingOnes(mask);
            uint hostsCount = (uint)(Math.Pow(2, hostBits) - 2);

            // Вычисление максимального номера хоста  
            uint maxHostNumber = hostsCount;

            Console.WriteLine("\nРезультаты расчета:");
            Console.WriteLine($"Адрес сети: {UintToIp(networkAddress)}");
            Console.WriteLine($"Доступно IP-адресов: {hostsCount}");
            Console.WriteLine($"Максимальный номер компьютера: {maxHostNumber}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    // Проверка корректности маски сети (должна быть последовательность 1, затем 0)
    static bool IsValidSubnetMask(string mask)
    {
        try
        {
            uint maskValue = IpToUint(mask);
            bool zeroFound = false;

            for (int i = 31; i >= 0; i--)
            {
                uint bit = (maskValue >> i) & 1;

                if (bit == 0)
                {
                    zeroFound = true;
                }
                else if (zeroFound)
                {
                    // Нашли 1 после 0 - маска невалидна  
                    return false;
                }
            }

            // Проверяем, что маска не состоит полностью из 0 или 1  
            return maskValue != 0 && maskValue != 0xFFFFFFFF;
        }
        catch
        {
            return false;
        }
    }

    // Подсчет количества ведущих 1 в маске  
    static int CountLeadingOnes(uint mask)
    {
        int count = 0;
        for (int i = 31; i >= 0; i--)
        {
            if (((mask >> i) & 1) == 1)
                count++;
            else
                break;
        }
        return count;
    }

    // Преобразование IP-адреса из строки в uint  
    static uint IpToUint(string ip)
    {
        string[] parts = ip.Split('.');
        if (parts.Length != 4)
            throw new FormatException("Неверный формат IP-адреса");

        uint result = 0;
        for (int i = 0; i < 4; i++)
        {
            if (!byte.TryParse(parts[i], out byte octet))
                throw new FormatException("Неверный формат IP-адреса");

            result = (result << 8) | octet;
        }
        return result;
    }

    // Преобразование uint в строку IP-адреса  
    static string UintToIp(uint ip)
    {
        return $"{(ip >> 24) & 0xFF}.{(ip >> 16) & 0xFF}.{(ip >> 8) & 0xFF}.{ip & 0xFF}";
    }
}
