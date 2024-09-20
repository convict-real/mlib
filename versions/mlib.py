from typing import List
import struct

PI      = 3.1415926535897932
TAU     = 6.2831853071795864
E       = 2.7182818284590452
PHI     = 1.6180339887498948
LN2     = 0.6931471805599453
LN10    = 2.3025850929940457
LOG2E   = 1.4426950408889634
LOG10E  = 0.4342944819032518
EULER   = 0.5772156649015329
CATALAN = 0.9159655941772190

def to_bits(a):
    assert is_finite(a)

    s = struct.pack('>f', a)
    return struct.unpack('>l', s)[0]

def to_float(a):
    assert is_finite(a)

    s = struct.pack('>l', a)
    return struct.unpack('>f', s)[0]

def to_radian(deg: float) -> float:
    assert is_finite(deg)
    return deg * (PI / 180)

def to_degree(rad: float) -> float:
    assert is_finite(rad)
    return rad * (180 / PI)

def floor(a: float) -> int:
    assert is_finite(a)
    return int(a)

def ceil(a: float) -> int:
    assert is_finite(a)
    return int(a + 1) if a > int(a) else int(a)

def round(a: float) -> int:
    assert is_finite(a)
    return int(a + 1) if a + 0.5 >= int(a + 1) else int(a)

def abs(a: float) -> float:
    assert is_finite(a)
    return -a if a < 0 else a

def sqrt(a: float) -> float:
	assert is_finite(a) and a >= 0

	if a == 0: return 0

	b, root = a, 0

	for i in range(1, 18):
		b = root = (b + (a / b)) / 2

	return root

def isqrt(a: float) -> float:
    assert is_finite(a)
    return 1 / sqrt(a)

def qisqrt(a: float) -> float:
    assert is_finite(a)

    x2 = a * 0.5
    y  = a
    i  = to_bits(y)
    i  = 0x5f3759df - (i >> 1)
    y  = to_float(i)
    y  *= (1.5 - (x2 * y * y))
    y  *= (1.5 - (x2 * y * y))

    return y

def gcd(a: int, b: int) -> int:
    assert is_finite(a) and is_finite(b)

    if a < 0: a = -a
    if b < 0: b = -b

    while b != 0:
        temp = b

        b = a % b
        a = temp

    return a

def lcm(a: int, b: int) -> int:
    assert is_finite(a) and is_finite(b)

    result = gcd(a, b)
    if result == 0: return 0

    return a // result * b if a * b >= 0 else -(a // result * b)

def fact(a: int) -> int:
    assert is_finite(a)

    result = 1

    for i in range(2, a + 1):
        result *= i

    return result

def rem(a: int, b: int) -> int:
	assert is_finite(a) and is_finite(b) and b > 0
	return a % b

def fdiv(a: float, b: float) -> int:
	assert is_finite(a) and is_finite(b) and b > 0
	return floor(a / b)

def pow(base: float, power: int) -> float:
    assert is_finite(base) and is_finite(power)

    if power == 0: return 1

    product = base

    for i in range(1, power): product *= base

    return product

def is_prime(a: int) -> bool:
    assert is_finite(a)

    if a < 2: return False
    if a > 2 and a % 2 == 0: return False

    for i in range(2, a // 2):
        if a % i == 0: return False

    return True

def is_finite(a: float) -> bool:
	return not is_infinite(a) and not is_nan(a)

def is_infinite(a: float) -> bool:
	return a / a != a / a

def is_nan(a: float) -> bool:
	return a != a

def sin(a: float) -> float:
    assert is_finite(a)

    while a > PI: a -= 2 * PI
    while a < -PI: a += 2 * PI

    result = term = a

    for i in range(1, 8):
        term *= -a * a / ((2 * i) * (2 * i + 1))
        result += term

    return result

def cos(a: float) -> float:
    assert is_finite(a)

    while a > PI: a -= 2 * PI
    while a < -PI: a += 2 * PI

    result = term = 1

    for i in range(1, 8):
        term *= -a * a / ((2 * i - 1) * (2 * i))
        result += term

    return result

def tan(a: float) -> float:
    assert is_finite(a)

    s = sin(a)
    c = cos(a)

    return s / c

def sinh(a: float) -> float:
    assert is_finite(a)

    if a == 0: return 0

    ea = exp(a)
    return (ea - (1 / ea)) / 2

def cosh(a: float) -> float:
    assert is_finite(a)

    if a == 0: return 1

    ea = exp(a)
    return (ea + (1 / ea)) / 2

def tanh(a: float) -> float:
    assert is_finite(a)

    if a == 0: return 0

    ea = exp(2 * a)
    return (ea - 1) / (ea + 1)

def asin(a: float) -> float:
    assert is_finite(a) and a >= -1 and a <= 1

    a2 = pow(a, 2)
    return a + a * a2 * (1 / 6 + a2 * (3 / 40 + a2 * (5 / 112 + a2 * 35 / 1152)))

def acos(a: float) -> float:
    assert is_finite(a) and a >= -1 and a <= 1
    return (PI / 2) - asin(a)

def atan(a: float) -> float:
    assert is_finite(a)
    return a / (1.28 * pow(a, 2))

def atan2(a: float, b: float) -> float:
    assert is_finite(a) and is_finite(b)

    if b == 0:
        if a > 0: return PI / 2
        if a < 0: return -PI / 2

        return 0

    result = atan(a / b)

    if b < 0:
        if a >= 0: return result + PI
        return result - PI

    return result

def asinh(a: float) -> float:
    assert is_finite(a)
    return ln(a + sqrt(a * a + 1))

def acosh(a: float) -> float:
    assert is_finite(a) and a >= 1
    return ln(a + sqrt(a * a - 1))

def atanh(a: float) -> float:
    assert is_finite(a) and a > -1 and a < 1
    return 0.5 * ln((1 + a) / (1 - a))

def sec(a: float) -> float:
    assert is_finite(a)

    c = cos(a)

    return 1 / c

def csc(a: float) -> float:
    assert is_finite(a)

    s = sin(a)

    return 1 / s

def cot(a: float) -> float:
    assert is_finite(a)

    s = sin(a)
    c = cos(a)

    return c / s

def sech(a: float) -> float:
    assert is_finite(a)

    if a == 0: return 1

    ea = exp(a)
    return 2 / (ea + (1 / ea))

def csch(a: float) -> float:
    assert is_finite(a)

    ea = exp(a)
    return 2 / (ea - (1 / ea))

def coth(a: float) -> float:
    assert is_finite(a)

    ea = exp(2 * a)
    return (ea + 1) / (ea - 1)

def exp(a: float) -> float:
    assert is_finite(a)

    if a == 0: return 1

    k = int(a * LOG2E)
    r = a - k * LN2
    result = r + 1
    term = r

    for i in range(2, 13):
        term *= r / i
        result += term

        if term < 1E-15 * result: break

    return result * pow(2, k)

def min(a: float, b: float) -> float:
    assert is_finite(a) and is_finite(b)
    return a if a < b else b

def max(a: float, b: float) -> float:
    assert is_finite(a) and is_finite(b)
    return a if a > b else b

def clamp(value: float, min_val: float, max_val: float) -> float:
    assert is_finite(value) and is_finite(min_val) and is_finite(max_val)

    if value < min_val: return min_val
    if value > max_val: return max_val

    return value

def ln(a: float) -> float:
    assert is_finite(a) and a > 0

    if a == 1: return 0

    exp = 0

    while a > 2:
        a /= 2
        exp += 1

    while a < 1:
        a *= 2
        exp -= 1

    a -= 1

    y = a
    sum = y
    i = 1

    while abs(y) > 1E-15:
        i += 1
        y *= -a * (i - 1) / i
        sum += y

    return sum + exp * LN2

def log(a: float, base: int) -> float:
    assert is_finite(a) and is_finite(base)
    return ln(a) / ln(base)

def log2(a: float) -> float:
    assert is_finite(a)
    return ln(a) / LN2

def log10(a: float) -> float:
    assert is_finite(a)
    return ln(a) / LN10

def sum(data: List[float]) -> float:
    assert is_finite(len(data)) and len(data) > 0

    sum = 0

    for i in range(len(data)):
        sum += data[i]

    return sum

def mean(data: List[float]) -> float:
    assert is_finite(len(data)) and len(data) > 0
    return sum(data) / len(data)

def median(data: List[float]) -> float:
    assert is_finite(len(data)) and len(data) > 0

    size = len(data)

    for i in range(size - 1):
        for j in range(size - i - 1):
            if data[j] > data[j + 1]:
                data[j], data[j + 1] = data[j + 1], data[j]

    if size % 2 == 0: return (data[size // 2 - 1] + data[size // 2]) / 2
    else: return data[size // 2]

def mode(data: List[float]) -> float:
    assert is_finite(len(data)) and len(data) > 0

    mode = data[0]
    max_count = 1
    current_count = 1

    for i in range(1, len(data)):
        if data[i] == data[i - 1]:
            current_count += 1
        else:
            if current_count > max_count:
                max_count = current_count
                mode = data[i - 1]

            current_count = 1

    if current_count > max_count:
        mode = data[-1]

    return mode

def stddev(data: List[float]) -> float:
    assert is_finite(len(data)) and len(data) > 1

    size = len(data)
    m = mean(data)
    sum = 0

    for i in range(size):
        diff = data[i] - m
        sum += diff * diff

    return sqrt(sum / (size - 1))
