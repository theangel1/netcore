using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class spGetCargos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE sp_obtener_cargo_paginacion(
                @NombreCargo nvarchar(500),
                @Ordenamiento nvarchar(500),
                @NumeroPagina int,
                @cantidadElementos int,
                @TotalRecords int OUTPUT,
                @TotalPaginas int OUTPUT
                )AS
                BEGIN
                    SET NOCOUNT ON
                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

                    DECLARE @Inicio int
                    DECLARE @Fin int

                    IF @NumeroPagina = 1
                        BEGIN
                            SET @Inicio = (@NumeroPagina*@CantidadElementos) - @CantidadElementos
                            SET @Fin = @NumeroPagina * @CantidadElementos
                        END
                    ELSE
                        BEGIN
                            SET @Inicio = ((@NumeroPagina*@CantidadElementos) - @CantidadElementos) +1
                            SET @Fin = @NumeroPagina * @CantidadElementos
                        END

                    CREATE TABLE #TMP(
                        rowNumber int IDENTITY(1,1),
                        ID int
                    )

                    DECLARE @SQL nvarchar(max)
                    SET @SQL = 'SELECT Id FROM Cargo '

                    IF @NombreCargo IS NOT NULL
                    BEGIN
                        SET @SQL = @SQL + ' WHERE Nombre LIKE ''%' + @NombreCargo + '%'' '
                    END

                    IF @Ordenamiento IS NOT NULL
                        BEGIN
                            SET @SQL = @SQL + ' ORDER BY ' + @Ordenamiento                        
                        END

                    INSERT INTO #TMP(ID)
                    EXEC sp_executesql @SQL

                    SELECT @TotalRecords = Count(*) FROM #TMP
                    IF @TotalRecords > @CantidadElementos
                        BEGIN
                            SET @TotalPaginas = @TotalRecords / @CantidadElementos
                            IF (@TotalRecords % @CantidadElementos) > 0
                                BEGIN
                                    SET @TotalPaginas = @TotalPaginas + 1
                                END
                        END
                ELSE
                    BEGIN
                        SET @TotalPaginas = 1
                        END
                        SELECT
                            c.Id,
                            c.Nombre,
                            c.Descripcion,
                            c.FechaCreacion,
                            c.FechaModificacion
                        FROM #TMP T INNER JOIN dbo.Cargo c on t.ID = c.Id                        
                        WHERE t.rowNumber >= @Inicio AND t.rowNumber <= @Fin
                END";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
