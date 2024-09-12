import React, { Component, ChangeEvent, KeyboardEvent} from "react";
import { Container, TextField, Button, List, ListItem, ListItemText, Paper, Typography } from '@mui/material';
import { IChatState } from "./Interfaces/IChatState";
import { IChatProps } from "./Interfaces/IChatProps";

class Chat extends Component<IChatProps, IChatState> {
    private ws: WebSocket | null = null;

    /**
     *
     */
    constructor(props: IChatProps) {
        super(props);

        this.state = {
            messages: [],
            input: "",
        };
    };

    componentDidMount(): void {
        this.ws =new WebSocket('ws://localhost:5120/api/chat');

        this.ws.onopen = () => {
            console.log('Connected to WebSockets server')
        };

        this.ws.onmessage = (event: MessageEvent) => {
            this.setState((prevState) => ({
                messages: [...prevState.messages, event.data]
            }));
        };

        this.ws.onclose = () => {
            console.log('Disconnect from WebSockets server');
        };
    };

    componentWillUnmount(): void {
        this.ws?.close();
    };

    handleInputChange = (event: ChangeEvent<HTMLInputElement>) => {
        this.setState({ input: event.target.value});
    };

    sendMessage = () => {
        const { input } = this.state;
        if(this.ws && input.trim()){
            this.ws.send(input);
            this.setState({ input });
        }
    };

    handleKeyDown = (event: KeyboardEvent<HTMLInputElement>) => {
        if(event.key === 'Enter') {
            this.sendMessage();
        }
    };

    render(): React.ReactNode {
        const { messages, input } = this.state;

        return(
            <Container maxWidth="sm" style={{ marginTop: '50px' }}>
                <Paper elevation={3} style={{ padding: '20px' }}>
                    <Typography variant="h4" component="h1" gutterBottom>
                        Chat
                    </Typography>
                    <List style={{ maxHeight: '300px', overflow: 'auto', marginBottom: '20px' }}>
                        {messages.map((msg, index) => (
                            <ListItem key={index}>
                            <ListItemText primary={msg} />
                            </ListItem>
                        ))}
                    </List>
                    <TextField
                        label="Your message"
                        variant="outlined"
                        fullWidth
                        value={input}
                        onChange={this.handleInputChange}
                        onKeyDown={this.handleKeyDown}
                        style={{ marginBottom: '10px' }}
                    />
                    <Button
                        variant="contained"
                        color="primary"
                        fullWidth
                        onClick={this.sendMessage}
                    >
                        Отправить
                    </Button>
                </Paper>
            </Container>
        );
    }
}

export default Chat;