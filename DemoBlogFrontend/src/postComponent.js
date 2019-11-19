export class PostComponentPropertiesBuilder {
    constructor(mode, post, markdown) {
        this.mode = mode;
        this.post = post;
        this.markdown = markdown;
    }

    isClickable() {
        switch (this.mode) {
            case "preview":
                return true;
            case "content":
                return false;
            default:
                return false;
        }
    }

    text() {
        let source = "";

        switch (this.mode) {
            case "preview":
                source = this.post.preview;
                break;
            case "content":
                source = this.post.preview + "\n" + this.post.content;
                break;
            default:
                return "";
        }

        let render = this.markdown.render(source);

        console.debug("Rendered markdown:");
        console.debug(render);

        return render;
    }
}