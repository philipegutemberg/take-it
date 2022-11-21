import { createNativeStackNavigator } from '@react-navigation/native-stack';

import Welcome from '../pages/Welcome';
import SignIn from '../pages/SignIn';
import Events from '../pages/Events';
import Event from '../pages/Event';
import Tickets from '../pages/Tickets';
import { StyleSheet } from 'react-native';
import Ticket from '../pages/Ticket';
import Gatekeeper from '../pages/Gatekeeper';
import ValidationResponse from '../pages/Gatekeeper/validationResponse';
import { createBottomTabNavigator } from '@react-navigation/bottom-tabs'
import { useContext } from 'react';
import { AuthContext } from '../context/AuthContext';
import { Entypo, Feather } from '@expo/vector-icons';
import Wallet from '../pages/Wallet';

const EventsStack = createNativeStackNavigator();

function EventsStackScreen() {
    return (
        <EventsStack.Navigator
        screenOptions={{
            headerBackTitleVisible: false,
            headerStyle: styles.header,
            headerTitleStyle: styles.headerTitle,
            headerTintColor: '#fff',
            headerTitleAlign: 'center'
        }}>
            <Stack.Screen
                name="Events"
                component={Events}
                options={{ 
                    headerShown: true,
                    title: "Eventos",
                    headerBackVisible: false
                }}
            />

            <Stack.Screen
                name="Event"
                component={Event}
                options={{
                    headerShown: true,
                    title: ""
                }}
            />
        </EventsStack.Navigator>
    );
}

const TicketsStack = createNativeStackNavigator();

function TicketsStackScreen() {
    return (
        <TicketsStack.Navigator screenOptions={{
            headerBackTitleVisible: false,
            headerStyle: styles.header,
            headerTitleStyle: styles.headerTitle,
            headerTintColor: '#fff',
            headerTitleAlign: 'center'
        }}>
            <Stack.Screen
                name="Tickets"
                component={Tickets}
                options={{
                    headerShown: true,
                    title: "Meus ingressos"
                }}
            />

            <Stack.Screen
                name="Ticket"
                component={Ticket}
                options={{
                    headerShown: true,
                    title: "Ingresso"
                }}
            />
        </TicketsStack.Navigator>
    );
}

const WalletStack = createNativeStackNavigator();

function WalletStackScreen() {
    return (
        <WalletStack.Navigator screenOptions={{
            headerBackTitleVisible: false,
            headerStyle: styles.header,
            headerTitleStyle: styles.headerTitle,
            headerTintColor: '#fff',
            headerTitleAlign: 'center'
        }}>
            <Stack.Screen
                name="WalletScreen"
                component={Wallet}
                options={{
                    headerShown: true,
                    title: "Minha carteira"
                }}
            />
        </WalletStack.Navigator>
    );
}

export default function Routes() {
    const {token} = useContext(AuthContext);

    if (!token)
        return (<StackNotLogged />);
    else if (token.role == 'Customer')
        return (<StackLogged />);
    else if (token.role == 'Gatekeeper')
        return (<StackGatekeeper />);
}

const Stack = createNativeStackNavigator();

function StackNotLogged() {
    return (
        <Stack.Navigator>
            <Stack.Screen
                name="Welcome"
                component={Welcome}
                options={{ 
                    headerShown: false 
                }}
            />

            <Stack.Screen
                name="SignIn"
                component={SignIn}
                options={{ 
                    headerShown: false 
                }}
            />
        </Stack.Navigator>
    );
}

function StackGatekeeper() {
    return (
        <Stack.Navigator screenOptions={{
            headerBackTitleVisible: false,
            headerStyle: styles.header,
            headerTitleStyle: styles.headerTitle,
            headerTintColor: '#fff',
            headerTitleAlign: 'center'
        }}>
            <Stack.Screen
                name="Gatekeeper"
                component={Gatekeeper}
                options={{ 
                    headerShown: true,
                    title: "Validar ingresso",
                    headerBackVisible: false
                }}
            />

            <Stack.Screen
                name="ValidationResponse"
                component={ValidationResponse}
                options={{ 
                    headerShown: true,
                    title: "Resultado validação",
                    headerBackVisible: false
                }}
            />
        </Stack.Navigator>
    );
}

const Tab = createBottomTabNavigator();

function StackLogged() {
    return (
        <Tab.Navigator screenOptions={{
            headerShown: false,
            tabBarActiveTintColor: '#d957d0',
            tabBarStyle: {
                paddingBottom: 5,
                paddingTop: 5
            }
        }}>
            <Tab.Screen 
                name="Home" 
                component={EventsStackScreen}
                options={{
                    tabBarIcon: ({size, color}) => (
                        <Entypo name="home" size={size} color={color} />
                    )
                }}
            />
            <Tab.Screen 
                name="Ingressos" 
                component={TicketsStackScreen}
                options={{
                    tabBarIcon: ({size, color}) => (
                        <Entypo name="ticket" size={size} color={color} />
                    )
                }}
            />

            <Tab.Screen 
                name="Carteira" 
                component={WalletStackScreen}
                options={{
                    tabBarIcon: ({size, color}) => (
                        <Entypo name="wallet" size={size} color={color} />
                    )
                }}
            />
        </Tab.Navigator>
    );
}

const styles = StyleSheet.create({
    header: {
        backgroundColor: '#7ED957'
    },
    headerTitle: {
        fontSize: 18,
        color: '#fff'
    }
});