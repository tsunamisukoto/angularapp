export interface EntityContext extends DbContext {
    customers: DbSet<Customer>;
}