CREATE DATABASE EmpresaNorte
GO
USE EmpresaNorte
GO
SET DATEFORMAT dmy;

CREATE TABLE Provincias(
    id_provincia INT IDENTITY (1,1) PRIMARY KEY,
    descripcion VARCHAR(50) NOT NULL
);

CREATE TABLE Barrios(
    id_barrio INT IDENTITY (1,1) PRIMARY KEY, 
    descripcion VARCHAR(50) NOT NULL,
    id_provincia INT NOT NULL,
    CONSTRAINT fk_barrio_provincia FOREIGN KEY (id_provincia)
        REFERENCES Provincias (id_provincia)
);

CREATE TABLE Tipos_Empleado(
    id_tipo_empleado INT IDENTITY (1,1) PRIMARY KEY,
    descripcion VARCHAR(50) NOT NULL
);

CREATE TABLE Sucursales(
    id_sucursal INT IDENTITY (1,1) PRIMARY KEY,
    direccion VARCHAR(50) NOT NULL,
    telefono VARCHAR(20) NOT NULL,
    email VARCHAR(50),
    id_barrio INT NOT NULL,
    CONSTRAINT fk_sucursal_id_barrio FOREIGN KEY (id_barrio)
        REFERENCES Barrios (id_barrio)
);

CREATE TABLE Empleados (
    id_empleado INT identity (1,1) PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL,
    apellido VARCHAR(50) NOT NULL,
    id_sucursal INT,
    id_tipo_empleado INT NOT NULL,
	dni varchar(10) not null,
	telefono varchar(20) not null,
	email varchar(50) not null,
	fecha_nac date not null,
	fecha_ingreso date not null,
    CONSTRAINT fk_empleado_id_sucursal FOREIGN KEY (id_sucursal)
        REFERENCES Sucursales (id_sucursal),
    CONSTRAINT fk_empleado_tipo_empleado FOREIGN KEY (id_tipo_empleado)
        REFERENCES Tipos_Empleado (id_tipo_empleado)
);

INSERT INTO Provincias (descripcion) VALUES
('Córdoba'),
('Buenos Aires'),
('Santa Fe'),
('Mendoza'),
('Tucumán'),
('San Luis'),
('San Juan'),
('Río Negro'),
('La Pampa'),
('Entre Ríos');

INSERT INTO Barrios (descripcion, id_provincia) VALUES
('Nueva Córdoba', 1),
('Alberdi', 1),
('Palermo', 2),
('Recoleta', 2),
('Fisherton', 3),
('Barrio Candioti', 3),
('Godoy Cruz', 4),
('Las Heras', 4),
('Yerba Buena', 5),
('Barrio Norte', 5),
('Centro', 6),
('San Luis Oeste', 6),
('Rawson', 7),
('Rivadavia', 7),
('General Roca', 8),
('Cipolletti', 8),
('Santa Rosa Centro', 9),
('Villa Alonso', 9),
('Paraná Centro', 10),
('San Agustín', 10);

INSERT INTO Tipos_Empleado (descripcion) VALUES 
('Administrativo'),
('Producción'),
('Depósito'),
('Contador'),
('Vendedor/a'),
('Atención al Cliente'),
('Cadete'),
('Mantenimiento'),
('Compras'),
('Marketing');

INSERT INTO Sucursales (direccion, telefono, email, id_barrio) VALUES
('Calle Comercio 100', '3511231231', 'sucursal1@gmail.com', 1),
('Calle Industria 456', '3512342342', 'sucursal2@gmail.com', 3),
('Calle Trabajo 789', '3514567810', 'sucursal3@gmail.com', 5),
('Calle Producción 321', '3514564564', 'sucursal4@gmail.com', 7),
('Calle Central 852', '3515675675', 'sucursal5@gmail.com', 9),
('Calle Sur 630', '3516786786', 'sucursal6@gmail.com', 11),
('Calle Norte 98', '3517897897', 'sucursal7@gmail.com', 13),
('Calle Esperanza 2025', '3518908908', 'sucursal8@gmail.com', 15),
('Calle Los Reartes 7971', '3514238557', 'sucursal9@gmail.com', 17),
('Calle Luis Lagos García 1213', '3514436390', 'sucursal10@gmail.com', 19);

INSERT INTO Empleados (nombre, apellido, id_sucursal, id_tipo_empleado, dni, telefono, email, fecha_nac, fecha_ingreso) VALUES
('Emiliano', 'Martínez', 1, 1, 32123456, '3518888808', 'martinez@gmail.com', '02/09/1992', '03/06/2022'),
('Lisandro', 'Martínez', 2, 2, '32123457', '3512222222', 'lmartinez@gmail.com', '18/01/1998', '03/06/2019'),
('Rodrigo', 'De Paul', 3, 5, '32123458', '3513333333', 'depaul@gmail.com', '24/05/1994', '11/06/2018'),
('Leandro', 'Paredes', 4, 4, '32123459', '3514444444', 'paredes@gmail.com', '29/06/1994', '05/09/2017'),
('Enzo', 'Fernández', 5, 3, '32123460', '3515555555', 'enzo@gmail.com', '17/01/2001', '24/09/2022'),
('Julián', 'Álvarez', 6, 6, '32123461', '3516666666', 'jalvarez@gmail.com', '31/01/2000', '03/06/2021'),
('Nicolás', 'Otamendi', 7, 9, '32123462', '3517777777', 'otamendi@gmail.com', '12/02/1988', '20/05/2009'),
('Cristian', 'Romero', 8, 2, '32123463', '3519999999', 'cromero@gmail.com', '27/04/1998', '03/06/2021'),
('Alexis', 'Mac Allister', 9, 3, '32123464', '3511231234', 'amacallister@gmail.com', '24/12/1998', '08/09/2019'),
('Gonzalo', 'Montiel', 10, 5, '32123465', '3512342345', 'gmontiel@gmail.com', '01/01/1997', '03/06/2021');

