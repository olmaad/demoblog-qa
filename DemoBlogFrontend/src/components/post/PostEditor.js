export const Modes = {
    editor: 0,
    preview: 1
};

export class PostEditorPropertiesBuilder {
    constructor(mode, post, markdown) {
        this.mode = mode;
        this.post = post;
        this.markdown = markdown;
    }

    headerText() {
        switch(this.mode) {
            case Modes.editor:
                return "Заголовок:";
            case Modes.preview:
                return "Предпросмотр:";
        }
    }

    switchButtonText() {
        switch(this.mode) {
            case Modes.editor:
                return "Предпросмотр";
            case Modes.preview:
                return "Редактор";
        }
    }

    text() {
        let preview = this.markdown.render(this.post.preview + "\n\n" + this.post.content);

        console.debug("Rendered editor preview:");
        console.debug(preview);

        return preview;
    }
}