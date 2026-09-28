CREATE PROCEDURE spListarProductos
AS
BEGIN
    SELECT 
        Id,
        Nombre,
        Precio,
        Stock,
        Categoria,
        Descripcion
    FROM Productos
END
GO


CREATE PROCEDURE spBuscarProducto
    @Id INT
AS
BEGIN
    SELECT 
        Id,
        Nombre,
        Precio,
        Stock,
        Categoria,
        Descripcion
    FROM Productos
    WHERE Id = @Id
END
GO


CREATE PROCEDURE spInsertarProducto
    @Nombre NVARCHAR(100),
    @Precio DECIMAL(10,2),
    @Stock INT,
    @Categoria NVARCHAR(100),
    @Descripcion NVARCHAR(500)
AS
BEGIN
    INSERT INTO Productos
        (Nombre, Precio, Stock, Categoria, Descripcion)
    VALUES
        (@Nombre, @Precio, @Stock, @Categoria, @Descripcion)
END
GO


CREATE PROCEDURE spActualizarProducto
    @Id INT,
    @Nombre NVARCHAR(100),
    @Precio DECIMAL(10,2),
    @Stock INT,
    @Categoria NVARCHAR(100),
    @Descripcion NVARCHAR(500)
AS
BEGIN
    UPDATE Productos
    SET
        Nombre = @Nombre,
        Precio = @Precio,
        Stock = @Stock,
        Categoria = @Categoria,
        Descripcion = @Descripcion
    WHERE Id = @Id
END
GO


CREATE PROCEDURE spEliminarProducto
    @Id INT
AS
BEGIN
    DELETE FROM Productos
    WHERE Id = @Id
END
GO