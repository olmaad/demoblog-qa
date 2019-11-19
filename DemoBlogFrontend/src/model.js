export class User {
    constructor() {
        this.id = -1;
        this.login = "";
        this.name = "";
    }

    static fromJson(json) {
        let user = new User();

        user.id = json.id;
        user.login = json.login;
        user.name = json.name;

        return user;
    }
}

export class Session {
    constructor() {
        this.valid = false;
        this.id = "";
        this.userId = -1;
    }

    static fromJson(json) {
        let session = new Session();

        session.valid = json.valid;
        session.id = json.id;
        session.userId = json.userId;

        return session;
    }
}

export class Post {
    constructor() {
        this.id = -1;
        this.userId = -1;
        this.title = "";
        this.preview = "";
        this.content = "";
    }

    static fromJson(json) {
        let post = new Post();

        post.id = json.id;
        post.userId = json.userId;
        post.title = json.title;
        post.preview = json.preview;
        post.content = json.content;

        return post;
    }
}

export class Comment {
    constructor() {
        this.id = -1;
        this.postId = -1;
        this.userId = -1;
        this.text = "";
    }

    static fromJson(json) {
        let comment = new Comment();

        comment.id = json.id;
        comment.postId = json.postId;
        comment.userId = json.userId;
        comment.text = json.text;

        return comment;
    }
}