import { createNativeStackNavigator } from '@react-navigation/native-stack';

import Welcome from '../pages/Welcome';
import SignIn from '../pages/SignIn';
import Events from '../pages/Events';
import Event from '../pages/Event';
import Tickets from '../pages/Tickets';
import { StyleSheet } from 'react-native';
import Ticket from '../pages/Ticket';
import Gatekeeper from '../pages/Gatekeeper';

const Stack = createNativeStackNavigator();

export default function Routes() {
    return (
        <Stack.Navigator screenOptions={{
            headerBackTitleVisible: false,
            headerStyle: styles.header,
            headerTitleStyle: styles.headerTitle,
            headerTintColor: '#fff',
            headerTitleAlign: 'center'
        }}>
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

            <Stack.Screen
                name="Gatekeeper"
                component={Gatekeeper}
                options={{ 
                    headerShown: true,
                    title: "Validar ingresso",
                    headerBackVisible: false
                }}
            />
        </Stack.Navigator>
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