CREATE DATABASE BACKENDAPI;
GO

USE BACKENDAPI;
GO

CREATE TABLE persona (
    persona_id INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(50),
    genero CHAR(1),
    telefono VARCHAR(20),
    direccion VARCHAR(100)
);
GO

CREATE TABLE clientes (
    cliente_id INT IDENTITY(1,1) PRIMARY KEY,
    persona_id INT FOREIGN KEY REFERENCES persona(persona_id),
    contrasena VARCHAR(50),
    estado VARCHAR(20)
);
GO

CREATE TABLE cuentas (
    cuenta_id INT IDENTITY(1,1) PRIMARY KEY,
    cliente_id INT FOREIGN KEY REFERENCES clientes(cliente_id),
    numero_cuenta VARCHAR(20),
    tipo_cuenta VARCHAR(50),
    saldo_inicial DECIMAL(10, 2),
    estado VARCHAR(20)
);
GO

SELECT * FROM cuentas
SELECT * FROM clientes

-- Crear la tabla movimientos con identidad en la columna movimiento_id
CREATE TABLE movimientos (
    movimiento_id INT IDENTITY(1,1) PRIMARY KEY,
    cuenta_id INT FOREIGN KEY REFERENCES cuentas(cuenta_id),
    fecha_movimiento DATETIME,
    tipo_movimiento VARCHAR(50),
    valor_movimiento DECIMAL(10, 2),
    saldo_disponible DECIMAL(10, 2)
);
GO

-- Insertar nuevos datos de prueba en la tabla persona
INSERT INTO persona (nombre, genero, telefono, direccion)
VALUES ('Juan Pérez', 'M', '555-1234', 'Calle Principal 123'),
       ('María Gómez', 'F', '555-5678', 'Avenida Central 456'),
       ('Carlos Ramírez', 'M', '555-9876', 'Paseo del Sol 789');
GO

-- Insertar nuevos datos de prueba en la tabla clientes
INSERT INTO clientes (persona_id, contrasena, estado)
VALUES (1, '123456', 'Activo'),
       (2, 'abcdef', 'Activo'),
       (3, 'qwerty', 'Inactivo');
GO

-- Insertar nuevos datos de prueba en la tabla cuentas
INSERT INTO cuentas (cliente_id, numero_cuenta, tipo_cuenta, saldo_inicial, estado)
VALUES (1, '123456789', 'Ahorros', 5000.00, 'Activa'),
       (1, '987654321', 'Corriente', 10000.00, 'Activa'),
       (2, '654321987', 'Ahorros', 7500.00, 'Inactiva'),
       (3, '456789123', 'Corriente', 12000.00, 'Activa');
GO

-- Insertar nuevos datos de prueba en la tabla movimientos
INSERT INTO movimientos (cuenta_id, fecha_movimiento, tipo_movimiento, valor_movimiento, saldo_disponible)
VALUES (1, '2022-02-01 10:30:00', 'Depósito', 1000.00, 6000.00),
       (1, '2022-02-05 15:45:00', 'Retiro', -500.00, 5500.00),
       (2, '2022-03-20 09:15:00', 'Transferencia', -2000.00, 8000.00),
       (3, '2022-06-10 14:20:00', 'Depósito', 3000.00, 10500.00),
       (4, '2022-07-05 11:10:00', 'Retiro', -1500.00, 10500.00);
GO

SELECT m.*
FROM movimientos m
JOIN cuentas c ON m.cuenta_id = c.cuenta_id
WHERE c.numero_cuenta = '987654321';

SELECT COUNT(*) AS cantidad_movimientos
FROM movimientos
WHERE cuenta_id = 1;

--Eliminacion de tablas
IF OBJECT_ID('movimientos', 'U') IS NOT NULL
    DROP TABLE movimientos;

IF OBJECT_ID('cuentas', 'U') IS NOT NULL
    DROP TABLE cuentas;

IF OBJECT_ID('clientes', 'U') IS NOT NULL
    DROP TABLE clientes;

IF OBJECT_ID('persona', 'U') IS NOT NULL
    DROP TABLE persona;
