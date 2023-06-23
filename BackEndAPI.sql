CREATE DATABASE BACKENDAPI
GO

USE BACKENDAPI
GO

CREATE TABLE persona (
id int primary key identity,
nombre varchar(20),
genero varchar(20),
edad int,
direccion varchar(70),
telefono varchar(12)
);
GO

SET IDENTITY_INSERT cliente ON

CREATE TABLE cliente(
clienteID int primary key identity,
personaId int,
contraseña varchar(50),
estado varchar(6),
foreign key (personaId) references persona(id)
);
GO

CREATE TABLE cuenta(
id int primary key identity,
clienteId int,
numeroCuenta int,
tipoCuenta varchar(30),
saldoInicial decimal(18,2),
estado varchar(30),
foreign key (clienteId) references cliente(clienteID)
);
GO

CREATE TABLE movimientos(
id int primary key identity,
cuentaId int,
fecha date,
tipoMovimiento varchar(100),
valor decimal(18,2),
saldo decimal(18,2),
foreign key (cuentaId) references cuenta(id)
);
GO

DROP DATABASE BACKENDAPI

INSERT INTO persona(nombre, genero, edad, direccion, telefono) values
('Jesus','hombre',33,'Santo Rosalio #111',8713477777)

SELECT * FROM persona

INSERT INTO cliente(personaId,contraseña,estado) values
(4,'53423','false')

SELECT * FROM cliente

INSERT INTO cuenta(clienteId, numeroCuenta, tipoCuenta, saldoInicial, estado) values
(1,'1230123','cheques',100.00,'true')

SELECT * FROM cuenta

INSERT INTO movimientos(cuentaId, fecha, tipoMovimiento, valor, saldo) values
(1, '2023-03-02', 'Retiro', 10, 1.00)

SELECT * FROM movimientos

EXEC sp_help 'cliente'