using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameVault.Migrations
{
    /// <inheritdoc />
    public partial class AddStoredProcedures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE OR ALTER PROCEDURE spListarProductos
                AS
                BEGIN
                    SELECT Id, Nombre, Precio, Stock, Categoria, Descripcion
                    FROM Productos;
                END;
            ");

                    migrationBuilder.Sql(@"
                CREATE OR ALTER PROCEDURE spBuscarProducto
                    @Id INT
                AS
                BEGIN
                    SELECT Id, Nombre, Precio, Stock, Categoria, Descripcion
                    FROM Productos
                    WHERE Id = @Id;
                END;
            ");

                    migrationBuilder.Sql(@"
                CREATE OR ALTER PROCEDURE spInsertarProducto
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
                        (@Nombre, @Precio, @Stock, @Categoria, @Descripcion);
                END;
            ");

                    migrationBuilder.Sql(@"
                CREATE OR ALTER PROCEDURE spActualizarProducto
                    @Id INT,
                    @Nombre NVARCHAR(100),
                    @Precio DECIMAL(10,2),
                    @Stock INT,
                    @Categoria NVARCHAR(100),
                    @Descripcion NVARCHAR(500)
                AS
                BEGIN
                    UPDATE Productos
                    SET Nombre = @Nombre,
                        Precio = @Precio,
                        Stock = @Stock,
                        Categoria = @Categoria,
                        Descripcion = @Descripcion
                    WHERE Id = @Id;
                END;
            ");

                    migrationBuilder.Sql(@"
                CREATE OR ALTER PROCEDURE spEliminarProducto
                    @Id INT
                AS
                BEGIN
                    DELETE FROM Productos WHERE Id = @Id;
                END;
            ");

                    migrationBuilder.Sql(@"
                CREATE OR ALTER PROCEDURE spRegistrarContacto
                    @Nombre NVARCHAR(100),
                    @Email NVARCHAR(255),
                    @Mensaje NVARCHAR(500)
                AS
                BEGIN
                    INSERT INTO Contactos
                        (Nombre, Email, Mensaje, Fecha, Leido)
                    VALUES
                        (@Nombre, @Email, @Mensaje, GETDATE(), 0);
                END;
            ");

                    migrationBuilder.Sql(@"
                CREATE OR ALTER PROCEDURE spMarcarContactoLeido
                    @Id INT
                AS
                BEGIN
                    UPDATE Contactos
                    SET Leido = 1
                    WHERE Id = @Id;
                END;
            ");

                    migrationBuilder.Sql(@"
                CREATE OR ALTER PROCEDURE spListarContactos
                AS
                BEGIN
                    SELECT Id, Nombre, Email, Mensaje, Fecha, Leido
                    FROM Contactos
                    ORDER BY Fecha DESC;
                END;
            ");

                    migrationBuilder.Sql(@"
                CREATE OR ALTER PROCEDURE spEliminarContacto
                    @Id INT
                AS
                BEGIN
                    DELETE FROM Contactos WHERE Id = @Id;
                END;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS spListarProductos;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS spBuscarProducto;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS spInsertarProducto;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS spActualizarProducto;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS spEliminarProducto;");

            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS spRegistrarContacto;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS spMarcarContactoLeido;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS spListarContactos;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS spEliminarContacto;");
        }
    }
}
