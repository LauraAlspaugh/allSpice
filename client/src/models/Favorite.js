export class Favorite {
    constructor(data) {
        this.id = data.id || __id
        this.accountId = data.accountId
        this.recipeId = data.recipeId
        this.creator = data.creator
        this.creatorId = data.creatorId
        this.recipe = data.recipe
        this.isFavorite = data.isFavorite
    }
}