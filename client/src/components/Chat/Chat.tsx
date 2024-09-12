import React, { Component, ChangeEvent, KeyboardEvent} from "react";
import { Container, TextField, Button, List, ListItem, ListItemText, Paper, Typography } from '@mui/material';
import { IChatState } from "./Interfaces/IChatState";
import { IChatProps } from "./Interfaces/IChatProps";

class Chat extends Component<IChatProps, IChatState> {
    private ws: WebSocket | null = null;

    /**
     * В конструкторе делаем инициализацию состояния компонента, так же задаем начальные значения для состояния.
     */
    constructor(props: IChatProps) {
        super(props);

        this.state = {
            messages: [],
            input: "",
        };
    };

    /*
    * Функции componentDidMount и componentWillUnmount управляют поведением компонента на разных этапах его существования: встраивание в DOM, и удаления из DOM.
    */
   // Вызывается сразу после встривания компонента в ДОМ.
    componentDidMount(): void {
        this.ws = new WebSocket('ws://localhost:5120/api/chat'); // Создаем объект ВебСокет и подключаемся к серверну.

        this.ws.onopen = () => {
            console.log('Connected to WebSockets server')
        };
        // Добавляем принятые сообщения в состояние компонента.
        this.ws.onmessage = (event: MessageEvent) => {
            this.setState((prevState) => ({
                messages: [...prevState.messages, event.data]
            }));
        };

        this.ws.onclose = () => {
            console.log('Disconnect from WebSockets server');
        };
    };

    // Вызывается перед удалением компонента из ДОМ.
    componentWillUnmount(): void {
        this.ws?.close();
    };
    // Обработчик дл события ввода. Срабатывает при каждом изменении значения в поле для ввода. Используем для обновления состояния, после каждого ввода символа.
    handleInputChange = (event: ChangeEvent<HTMLInputElement>) => {
        this.setState({ input: event.target.value});
    };

    // Метод для отправки сообщения серверу, через ВебСокет соединение.
    sendMessage = () => {
        console.log('start senMessage function');
        const { input } = this.state;
        if(this.ws && input.trim()){
            this.ws.send(input);
            this.setState({ input });
        }
    };

    // Обработчик для события нажатия клавиш в поле ввода. При нажатии Enter - отправляем сообщение серверу.
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