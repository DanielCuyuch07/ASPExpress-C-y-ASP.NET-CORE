use DBBANCOLOMBIA;

select * from Personas;

CREATE TABLE Departamentos(
	idDepartamento INT IDENTITY(1,1) PRIMARY KEY,
	departamento nvarchar(50),
);

CREATE TABLE Inversiones (
    idInversion INT IDENTITY(1,1) PRIMARY KEY,
    nombreInversion NVARCHAR(50)
);

CREATE TABLE Clientes (
    idCliente INT IDENTITY(1,1) PRIMARY KEY,
    NombreCliente NVARCHAR(50),
    NumeroDeCuenta CHAR(8),
    CorreoElectronico NVARCHAR(100),
    Saldo DECIMAL(10, 2),
    idInversion INT,
    FOREIGN KEY (idInversion) REFERENCES Inversiones(idInversion)
);

Select * from Clientes;

INSERT INTO Departamentos (departamento) VALUES
('Ventas'),
('Recursos Humanos'),
('Finanzas'),
('Desarrollo'),
('Marketing'),
('Operaciones'),
('Tecnología'),
('Producción'),
('Calidad'),
('Logística'),
('Administración'),
('Legal'),
('Compras'),
('Servicio al Cliente'),
('Investigación y Desarrollo'),
('Diseño'),
('Mercadotecnia'),
('Contabilidad');

DELETE FROM Clientes WHERE idCliente = 1;


INSERT INTO Inversiones (nombreInversion) VALUES
('Fondos de Pensiones'),
('Acciones Tecnológicas'),
('Bienes Raíces'),
('Bonos Gubernamentales'),
('Fondos Mutuos'),
('Oro'),
('Criptomonedas'),
('Acciones Energéticas'),
('Bonos Corporativos'),
('Metales Preciosos'),
('Acciones Farmacéuticas'),
('Inversiones Sostenibles'),
('Acciones de Consumo'),
('Bienes Duraderos'),
('Acciones de Viajes y Ocio'),
('Bonos Municipales'),
('Inversiones Alternativas'),
('Fondos de Riesgo');

INSERT INTO Clientes (NombreCliente, NumeroDeCuenta, CorreoElectronico, Saldo, idInversion) VALUES
('Juan Pérez', '12345678', 'juan.perez@example.com', 5000.00, 1),
('María López', '87654321', 'maria.lopez@example.com', 7000.00, 2),
('Pedro García', '98765432', 'pedro.garcia@example.com', 3000.00, 3),
('Ana Martínez', '23456789', 'ana.martinez@example.com', 10000.00, 4),
('Carlos Rodríguez', '34567890', 'carlos.rodriguez@example.com', 8000.00, 5),
('Laura Sánchez', '45678901', 'laura.sanchez@example.com', 6000.00, 6),
('Javier Fernández', '56789012', 'javier.fernandez@example.com', 9000.00, 7),
('Sofía Gómez', '67890123', 'sofia.gomez@example.com', 12000.00, 8),
('Diego Torres', '78901234', 'diego.torres@example.com', 4000.00, 9),
('Elena Ramírez', '89012345', 'elena.ramirez@example.com', 8500.00, 10),
('Martín Díaz', '90123456', 'martin.diaz@example.com', 11000.00, 11),
('Luisa Castro', '01234567', 'luisa.castro@example.com', 6200.00, 12),
('Roberto Navarro', '13572468', 'roberto.navarro@example.com', 7300.00, 13),
('Julia Flores', '24681357', 'julia.flores@example.com', 8800.00, 14),
('Fernando Ruiz', '35792468', 'fernando.ruiz@example.com', 9500.00, 15),
('Marta González', '46813579', 'marta.gonzalez@example.com', 5100.00, 16),
('Alejandro Soto', '57924680', 'alejandro.soto@example.com', 6800.00, 17),
('Patricia Herrera', '68035791', 'patricia.herrera@example.com', 7700.00, 18);
