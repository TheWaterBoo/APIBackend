CREATE DATABASE BACKENDAPI

USE BACKENDAPI

CREATE TABLE persona (
id int primary key identity,
nombre varchar(100),
genero varchar(20),
edad int,
direccion varchar(255),
telefono varchar(100)
);

CREATE TABLE cliente(
clienteID int primary key,
contraseña varchar(100),
estado varchar(20),
foreign key (clienteID) references persona(id)
);

CREATE TABLE cuenta(
id int primary key,
numeroCuenta varchar(30),
tipoCuenta varchar(30),
saldoInicial int,
estado varchar(30),
foreign key (id) references cliente(clienteID)
);

CREATE TABLE movimientos(
id int primary key,
fecha date,
tipoMovimiento varchar(100),
valor int,
saldo int,
foreign key (id) references cuenta(id)
);

DROP DATABASE BACKENDAPI

INSERT INTO persona(nombre, genero, edad, direccion, telefono) values
('mary','mujer',28,'si','Otavalo s/n y principal',098254785)

SELECT * FROM persona

INSERT INTO cliente(clienteID,contraseña,estado) values
(3,'9887','false')

SELECT * FROM cliente

INSERT INTO cuenta(id, numeroCuenta, tipoCuenta, saldoInicial, estado) values
(1,'478758','ahorro','2000','true')

SELECT * FROM cuenta