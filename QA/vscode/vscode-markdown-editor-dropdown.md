# Enable Editor Selector for Markdown Files in VS Code

## Question

When I double click the MD file in the explorer pane, the preview is open; how can I enable a dropdown to select editor for markdown file at the top right corner?

## Answer

When you open a Markdown file and it shows the preview, the "Open With" dropdown you're looking for is controlled by a couple of VS Code settings. Here's how to get it and change the default:

### Why preview opens by default

VS Code likely has Markdown files associated with the preview editor. Double-click opens whatever the registered default editor is.

### Enabling the editor-selector dropdown (top-right "..." → "Reopen Editor With")

There isn't a permanent always-visible dropdown, but you have these options:

**1. Reopen the current file with a different editor**

- With the file open, click the **`...`** (More Actions) menu at the top-right of the editor tab area → **Reopen Editor With...**
- Or right-click the file in the Explorer → **Open With...**

This shows a picker: *Text Editor*, *Markdown Preview*, and lets you *Configure default editor for `*.md`*.

**2. Set the default so double-click opens the text editor (source)**

Open Settings (JSON) — `Cmd+Shift+P` → **Preferences: Open User Settings (JSON)** — and add:

```json
"workbench.editorAssociations": {
  "*.md": "default"
}
```

- `"default"` = the built-in text editor (edit the source).
- `"vscode.markdown.preview.editor"` = always open the rendered preview.

**3. Handy shortcuts once a `.md` is open** <------------------------------------------------------------------------

- `Cmd+Shift+V` — open the full preview
- `Cmd+K V` — open preview **to the side** (edit source + live preview side by side)

### Quick recommendation

If your goal is "double-click should let me edit, and I open preview when I want it," set the association to `"default"` (option 2) and use `Cmd+K V` for a side-by-side live preview. If you specifically want the picker each time, right-click → **Open With...** and choose **Configure default editor** to pin whichever behavior you prefer.
