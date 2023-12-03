export class Recipe {
    constructor(data) {
        this.id = data.id
        this.createdAt = new Date(data.createdAt).toLocaleDateString()
        this.updatedAt = new Date(data.updatedAt).toLocaleDateString()
        this.title = data.title
        this.instructions = data.instructions
        this.category = data.category
        this.creator = data.creator
        this.creatorId = data.creatorId
        this.img = data.img
        this.favorites = data.favorites
        this.isFavorite = data.isFavorite
    }
}