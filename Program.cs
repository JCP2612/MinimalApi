using Microsoft.EntityFrameworkCore;
using ProductsNet.Source.Application;
using ProductsNet.Source.Application.DTO;
using ProductsNet.Source.Domain;
using ProductsNet.Source.Infraestructure;
using ProductsNet.Source.Infraestructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy => { policy.WithOrigins("http://localhost:8100").AllowAnyHeader().AllowAnyMethod(); });
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<ProductRepository>();
builder.Services.AddScoped<FavoritesRepository>();

builder.Services.AddScoped<AddUserUseCase>();
builder.Services.AddScoped<LoginUseCase>();
builder.Services.AddScoped<AddCategoryUseCase>();
builder.Services.AddScoped<AddProductCase>();
builder.Services.AddScoped<AddFavoriteUseCase>();
builder.Services.AddScoped<DeleteFavoriteUseCase>();
builder.Services.AddScoped<UpdateCategoryUseCase>();
builder.Services.AddScoped<UpdateProductUseCase>();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowSpecificOrigin");

//Rutas 

app.MapPost("/api/register", async (CreateUserDTO createUserDTO, AddUserUseCase addCase) =>
{
    try
    {
        var response = await addCase.ExecuteAsync(createUserDTO);
        return Results.Ok(response);
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(new { message = ex.Message });
    }
});

app.MapPost("/api/login", async (LoginDTO loginDTO, LoginUseCase loginCase) =>
{
    try
    {
        var response = await loginCase.ExecuteAsync(loginDTO);
        if (response == null)
        {
            //TODO validar mensajes
            return Results.BadRequest(new { message = "No se ha podido autenticar" });
        }
        return Results.Ok(response);
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(new { message = ex.Message });
    }
});

app.MapPost("/api/category", async (Category category, AddCategoryUseCase categoryUseCase) =>
{
    try
    {
        await categoryUseCase.ExecuteAsync(category);
        return Results.Ok(category);
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(new { message = ex.Message });
    }
});

app.MapGet("/api/category", async (CategoryRepository repository) =>
{
    var categories = await repository.GetAllCategoryAsync();
    return Results.Ok(categories);
});


app.MapGet("/api/category/{id:int}", async (int id, CategoryRepository repository) =>
{
    var category = await repository.GetCategoryByIdAsync(id);
    return category is not null ? Results.Ok(category) : Results.NotFound();
});

app.MapPatch("/api/category/{id:int}", async (int id, Category category, CategoryRepository repository, UpdateCategoryUseCase updateCategoryUseCase) =>
{
    var existingCategory = await repository.GetCategoryByIdAsync(id);
    if (existingCategory is null) return Results.NotFound();
    var response = await updateCategoryUseCase.ExecuteAsync(existingCategory, category);
    return Results.Ok(response);
});

app.MapDelete("/api/category/{id:int}", async (int id, CategoryRepository repository) =>
{
    await repository.DeleteCategoryAsync(id);
    return Results.NoContent();
});

app.MapGet("/api/products", async (int? userId, ProductRepository repository) =>
{
    var products = await repository.GetAllProductsAsync(userId);
    return Results.Ok(products);
});

app.MapGet("/api/products/{id:int}", async (int id, ProductRepository repository) =>
{
    var category = await repository.GetProductByIdAsync(id);
    return category is not null ? Results.Ok(category) : Results.NotFound();
});

app.MapPost("/api/products", async (CreateProductDTO createProductDTO, AddProductCase addProductCase) =>
{
    try
    {
        var product = new Product
        {
            Name = createProductDTO.Name,
            Price = createProductDTO.Price,
            Description = string.IsNullOrWhiteSpace(createProductDTO.Description) ? "El usuario no ha ingresado una descripcion" : createProductDTO.Description,
            Image = string.IsNullOrWhiteSpace(createProductDTO.Image) ? "http://images.net/undefined.webp" : createProductDTO.Image
        };
        await addProductCase.ExecuteAsync(product, createProductDTO.CategoryIds);
        return Results.Ok(product);
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(new { message = ex.Message });
    }
});

app.MapPatch("/api/products/{id: int}", async (int id, UpdateProductDTO updateProductDTO, UpdateProductUseCase updateCase, ProductRepository repository) =>
{
    try
    {
        var existingProduct = await repository.GetProductByIdAsync(id);
        if (existingProduct is null) return Results.NotFound();

        var response = await updateCase.ExecuteAsync(updateProductDTO, existingProduct);
        return Results.Ok(response);
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(new { message = ex.Message });
    }
});

app.MapDelete("/api/products/{id:int}", async (int id, ProductRepository repository) =>
{
    await repository.DeleteProductAsync(id);
    return Results.NoContent();
});

app.MapPost("/api/favorite", async (CreateFavoriteDTO createFavoriteDTO, UserRepository userRepository, ProductRepository productRepository, AddFavoriteUseCase addFavoriteUseCase) =>
{
    try
    {
        var user = await userRepository.GetUserByIdAsync(createFavoriteDTO.UserId);
        if (user == null) return Results.NotFound();
        var product = await productRepository.GetProductByIdAsync(createFavoriteDTO.ProductId);
        if (product == null) return Results.NotFound();
        var response = await addFavoriteUseCase.ExecuteAsync(product, user);
        return Results.Ok(response);
    }
    catch (ArgumentException ex) { return Results.BadRequest(new { message = ex.Message }); }
});

app.MapDelete("/api/favorite/", async (int userId, int productId, FavoritesRepository repository, UserRepository userRepository, ProductRepository productRepository, DeleteFavoriteUseCase deleteUseCase) =>
{
    var user = await userRepository.GetUserByIdAsync(userId);
    var product = await productRepository.GetProductByIdAsync(productId);
    if (user != null && product != null)
    {
        var response = await deleteUseCase.ExecuteAsync(product, user);
        if (response) return Results.NoContent();
    }
    return Results.NotFound();
});

app.MapGet("/api/wish", async (int? userId, FavoritesRepository repository, UserRepository userRepository) =>
{
    if (userId != null)
    {
        var user = await userRepository.GetUserByIdAsync((int)userId);
        if (user != null)
        {
            var response = await repository.GetAllFavoriteByUserAsync(user);
            return Results.Ok(response);
        }
    }
    return Results.NotFound();
});

//fin rutas
app.Run();

